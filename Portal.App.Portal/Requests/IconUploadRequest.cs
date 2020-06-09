using Portal.App.Portal.Messages;
using Portal.App.Portal.Services;
using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Structure;
using Portal.Structure.Requests;
using System;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class IconUploadRequest : CommonDependent, IRequest<IconPost, Icon> {

        public static readonly string SAVE_PATH_TEMPLATE = "Data/Icons/{0}.{1}";

        private IIconService IconService { get; }
        private IWebsiteState WebsiteState { get; }
        private IFileReceiver FileReceiver { get; }

        public IconUploadRequest(IConnectionFactory ConnectionFactory, IIconService IconService,
            IWebsiteState WebsiteState, IFileReceiver FileReceiver) : base(ConnectionFactory) {
            this.IconService = IconService;
            this.WebsiteState = WebsiteState;
            this.FileReceiver = FileReceiver;
        }


        public Icon Process(IconPost model) {
            this.NeedNotNull(model, "uploaded icon");
            IconService.ValidateIconPost(model);
            IPostedFile file = FileReceiver.GetPostedFiles().FirstOrDefault();
            Icon icon = BuildIconFromMessage(model, file);
            IconService.ValidateIconPostFile(icon, file);

            Icon newIcon;
            using (IConnection connection = ConnectionFactory.Create()) {
                CheckAgainstExistingIcons(icon, connection);
                newIcon = UpdateDatabase(icon, connection);
                SaveFile(file, newIcon);
            }
            return newIcon;
        }

        private Icon BuildIconFromMessage(IconPost iconPost, IPostedFile file) {
            Icon icon = new Icon() {
                Id = iconPost.Id,
                Name = iconPost.Name,
                Link = iconPost.Link
            };

            // Force DB name to be correctly formatted
            icon.Name = PortalUtility.UnUrlFormat(PortalUtility.UrlFormat(icon.Name));

            if (file != null) {
                icon.Image = PortalUtility.GetImageExtension(file.ContentType);
            }
            return icon;
        }

        private void CheckAgainstExistingIcons(Icon icon, IConnection connection) {
            Icon existing = connection.IconByName(icon.Name);
            if (existing != null && existing.Id != icon.Id) {
                throw new PortalException(IconResult.FAIL_ALREADY_EXISTS,
                    string.Format("Icon Name '{0}' already exists", icon.Name));
            }
            if (icon.IsNew == false) {
                existing = connection.IconById(icon.Id);
                if (existing == null) {
                    throw new PortalException(IconResult.FAIL_DOES_NOT_EXIST,
                        string.Format("Icon with Id {0} does not exist", icon.Id));
                }
                icon.Image = icon.Image ?? existing.Image; // make sure history has image
            }
        }

        private Icon UpdateDatabase(Icon icon, IConnection connection) {
            if (icon.IsNew) {
                icon.DateChanged = DateTime.Now;
                icon.DateCreated = DateTime.Now;
                connection.Icons.Add(icon);
                connection.Log("Portal", string.Format("Icon Added: {0}", icon.Name));
                connection.SaveChanges();
            }

            Icon recordedIcon = connection.IconByName(icon.Name);
            if (recordedIcon == null) {
                throw new PortalException(IconResult.FAIL_NOT_FOUND_AFTER_ADDED,
                    string.Format("Icon '{0}' not found after added", icon.Name));
            }
            if (icon.IsNew == false) {
                recordedIcon.DateChanged = DateTime.Now;
                recordedIcon.Name = icon.Name;
                recordedIcon.Link = icon.Link;
                recordedIcon.Image = icon.Image;
                connection.Log("Portal", string.Format("Icon Updated: {0}", icon.Name));
            }

            IconHistory history = recordedIcon.ToHistory();
            connection.IconHistories.Add(history);
            connection.SaveChanges();

            return recordedIcon;
        }

        private void SaveFile(IPostedFile file, Icon newIcon) {
            if (file != null) {
                string path = WebsiteState.GetPath(string.Format(
                    SAVE_PATH_TEMPLATE, newIcon.Id, newIcon.Image));
                file.SaveAs(path);
            }
        }

    }

}
