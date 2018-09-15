using Portal;
using Portal.Data;
using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalWebsite.Data.Logic.Portal {

    public static class IconFormPostExtensions {

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

            IPostedFile file = form.GetPostedFile();
            if (icon.IsNew && file == null) {
                throw new ArgumentNullException("Icon Image File");
            }

            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new ArgumentOutOfRangeException(string.Format("Icon image is too large (limit {0}MB).", MAX_ICON_MB));
            }

            using (IConnection connection = connectionFactory.Invoke()) {
                Icon findExisting = Query.GetIconByName(icon.Name, connection);
                if (findExisting != null && findExisting.Id != icon.Id) {
                    throw new PortalException(string.Format("Icon Name '{0}' already exists.", icon.Name));
                }
                if (icon.IsNew == false) {
                    findExisting = Query.GetIconById(icon.Id, connection);
                    if (findExisting == null) {
                        throw new PortalException(string.Format("Icon with Id {0} does not exist.", icon.Id));
                    }
                }

                connection.ExecuteNonQuery(icon.BuildUpdateQuery());

                if (file != null) {
                    Icon newIcon = Query.GetIconByName(icon.Name, connection);
                    if (newIcon == null)
                        throw new PortalException(string.Format("Icon '{0}' not found.", icon.Name));
                    file.SaveAs(Path.Combine(basePath,
                        string.Format("Portal/Icons/{0}.{1}", newIcon.Id, newIcon.Image)
                    ));
                }
            }
        }

    }

}