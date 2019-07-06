using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;
using Portal.Requests.Portal.Results;
using System;
using System.IO;

namespace Portal.Requests.Portal {

    public class IconUploadRequest : FileReceiverDependentBase, IRequest<Icon, IconUploadRequestResult> {

        /// <summary>
        /// Upload Limit.
        /// </summary>
        private static readonly int MAX_ICON_MB = 10;

        public IconUploadRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState,
            IFileReceiver FileReceiver)
            : base(ConnectionFactory, WebsiteState, FileReceiver) {
        }

        public IconUploadRequestResult Process(Icon model) {
            model.ValidateData();

            // Force DB name to be correctly formatted
            model.Name = PortalUtility.UnUrlFormat(PortalUtility.UrlFormat(model.Name));

            IPostedFile file = GetAndCheckFile(model);

            Icon newIcon;
            using (IConnection connection = ConnectionFactory.Create()) {
                CheckAgainstExistingIcons(connection, model);
                newIcon = UpdateDatabase(connection, model);
                SaveFile(file, newIcon);
            }
            return new IconUploadRequestResult() {
                PostedFile = file,
                SubmittedIcon = model,
                SavedIcon = newIcon
            };
        }

        private IPostedFile GetAndCheckFile(Icon model) {
            IPostedFile file = FileReceiver.GetPostedFile();
            if (model.IsNew && file == null) {
                throw new ArgumentNullException("Icon Image File");
            }

            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new ArgumentOutOfRangeException("Image Upload",
                    string.Format("Is too large (limit {0}MB)", MAX_ICON_MB));
            }
            return file;
        }

        private void CheckAgainstExistingIcons(IConnection connection, Icon model) {
            Icon existing = connection.GetIconByName(model.Name);
            if (existing != null && existing.Id != model.Id) {
                throw new PortalException(string.Format("Icon Name '{0}' already exists", model.Name));
            }
            if (model.IsNew == false) {
                existing = connection.GetIconById(model.Id);
                if (existing == null) {
                    throw new PortalException(string.Format("Icon with Id {0} does not exist", model.Id));
                }
                model.Image = model.Image ?? existing.Image; // make sure history has image
            }
        }

        private Icon UpdateDatabase(IConnection connection, Icon model) {
            connection.ExecuteNonQuery(BuildUpdateQuery(model), QueryOptions.Log);
            Icon newIcon = connection.GetIconByName(model.Name);
            if (newIcon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found after added", model.Name));
            }
            connection.ExecuteNonQuery(BuildHistoryInsertQuery(newIcon, model.IsNew));
            return newIcon;
        }

        private void SaveFile(IPostedFile file, Icon newIcon) {
            if (file != null) {
                file.SaveAs(Path.Combine(WebsiteState.WebsitePath,
                    string.Format("Portal/Icons/{0}.{1}", newIcon.Id, newIcon.Image)
                ));
            }
        }

        private string BuildUpdateQuery(Icon model) {
            DatabaseChangeQuery query = new DatabaseChangeQuery(
                model.IsNew ? QueryType.INSERT : QueryType.UPDATE
                , "PortalIcon");

            query.AddField("Name", model.Name);
            query.AddField("Link", model.Link);
            query.AddField("DateChanged", PortalUtility.SqlTimestamp, IsQuoted: false);
            if (model.Image != null) {
                query.AddField("Image", model.Image);
            }
            if (model.IsNew) {
                query.AddField("DateCreated", PortalUtility.SqlTimestamp, IsQuoted: false);
            } else {
                query.WhereClause = "WHERE Id=" + model.Id;
            }

            return query.Build();
        }

        public static string BuildHistoryInsertQuery(Icon icon, bool isNew) {
            DatabaseChangeQuery query = new DatabaseChangeQuery(
                QueryType.INSERT, "PortalIconHistory");
            query.AddField("IconId", icon.Id.ToString(), IsQuoted: false);
            query.AddField("Name", icon.Name);
            query.AddField("Link", icon.Link);
            query.AddField("Image", icon.Image);
            query.AddField("IsNew", isNew ? "1" : "0", IsQuoted: false);
            query.AddField("DateUpdated", PortalUtility.SqlTimestamp, IsQuoted: false);
            return query.Build();
        }

    }

}
