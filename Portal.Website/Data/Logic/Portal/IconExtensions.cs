using Portal;
using Portal.Data;
using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Portal.Website.Data.Logic.Portal {

    public static class IconExtensions {

        /// <summary>
        /// Build an INSERT INTO or UPDATE statement for the current state of this Icon.
        /// </summary>
        public static string BuildUpdateQuery(this Icon icon) {
            DatabaseUpdateQuery query = new DatabaseUpdateQuery(icon.IsNew
                    ? DatabaseUpdateQuery.QueryType.INSERT
                    : DatabaseUpdateQuery.QueryType.UPDATE
                , "PortalIcon");

            query.AddField("Name", icon.Name);
            query.AddField("Link", icon.Link);
            query.AddField("DateChanged", PortalUtility.SqlTimestamp, false);
            if (icon.Image != null) {
                query.AddField("Image", icon.Image);
            }
            if (icon.IsNew) {
                query.AddField("DateCreated", PortalUtility.SqlTimestamp, false);
            } else {
                query.WhereClause = "WHERE Id=" + icon.Id;
            }

            return query.Build();
        }

        /// <summary>
        /// Builds a query to insert the current state of this Icon for a history record.
        /// </summary>
        public static string BuildHistoryInsertQuery(this Icon icon, bool isNew) {
            DatabaseUpdateQuery query = new DatabaseUpdateQuery(
                DatabaseUpdateQuery.QueryType.INSERT,
                "PortalIconHistory");
            query.AddField("IconId", icon.Id.ToString(), false);
            query.AddField("Name", icon.Name);
            query.AddField("Link", icon.Link);
            query.AddField("Image", icon.Image);
            query.AddField("IsNew", isNew ? "1" : "0", false);
            query.AddField("DateUpdated", PortalUtility.SqlTimestamp, false);
            return query.Build();
        }


        /// <summary>
        /// Sets a new Icon with the values of the Icon FormPost.
        /// </summary>
        public static Icon GetIcon(this IFormPost formPost) {
            Icon result = new Icon() {
                Id = string.IsNullOrWhiteSpace(formPost["Id"]) ? -1 : int.Parse(formPost["Id"]),
                Name = formPost["Name"],
                Link = formPost["Link"]
            };
            IPostedFile file = formPost.GetPostedFile();
            if (file != null) {
                result.Image = PortalUtility.GetImageExtension(file.ContentType);
            }
            return result;
        }

        /// <summary>
        /// Upload Limit.
        /// </summary>
        private static readonly int MAX_ICON_MB = 10;

        /// <summary>
        /// Save an Icon, images saved to the website directory.
        /// </summary>
        public static void UploadIcon(this IFormPost form, Func<IConnection> connectionFactory) {
            form.UploadIcon(connectionFactory, PortalUtility.SitePath);
        }

        /// <summary>
        /// Saves an Icon.
        /// </summary>
        public static void UploadIcon(this IFormPost form, Func<IConnection> connectionFactory, string basePath) {
            Icon icon = form.GetIcon();
            icon.ValidateData();

            // Force DB name to be correctly formatted
            icon.Name = PortalUtility.UnUrlFormat(PortalUtility.UrlFormat(icon.Name));

            IPostedFile file = form.GetPostedFile();
            if (icon.IsNew && file == null) {
                throw new ArgumentNullException("Icon Image File");
            }

            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new ArgumentOutOfRangeException(string.Format("Icon image is too large (limit {0}MB).", MAX_ICON_MB));
            }

            using (IConnection connection = connectionFactory.Invoke()) {
                Icon findExisting = connection.GetIconByName(icon.Name);
                if (findExisting != null && findExisting.Id != icon.Id) {
                    throw new PortalException(string.Format("Icon Name '{0}' already exists.", icon.Name));
                }
                if (icon.IsNew == false) {
                    findExisting = connection.GetIconById(icon.Id);
                    if (findExisting == null) {
                        throw new PortalException(string.Format("Icon with Id {0} does not exist.", icon.Id));
                    }
                    icon.Image = icon.Image ?? findExisting.Image; //make sure history has image
                }

                connection.ExecuteNonQuery(icon.BuildUpdateQuery(), QueryOptions.Log);
                Icon newIcon = connection.GetIconByName(icon.Name);
                if (newIcon == null) {
                    throw new PortalException(string.Format("Icon '{0}' not found.", icon.Name));
                }
                connection.ExecuteNonQuery(newIcon.BuildHistoryInsertQuery(icon.IsNew));

                if (file != null) {
                    file.SaveAs(Path.Combine(basePath,
                        string.Format("Portal/Icons/{0}.{1}", newIcon.Id, newIcon.Image)
                    ));
                }
            }
        }

    }

}
