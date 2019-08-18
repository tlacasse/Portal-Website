using Portal.App.Portal.Messages;
using Portal.App.Portal.Models;
using Portal.App.Portal.Services;
using Portal.App.Portal.Tables;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Requests;
using System;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class IconUploadRequest : DependentBase, IRequestIn<IconPost> {

        public static readonly string SAVE_PATH_TEMPLATE = "Data/Icons/{0}.{1}";

        private IWebsiteState WebsiteState { get; }
        private IIconTable IconTable { get; }
        private IIconHistoryTable IconHistoryTable { get; }
        private IFileReceiver FileReceiver { get; }
        private IIconService IconService { get; }

        public IconUploadRequest(IActiveContext ActiveContext, IWebsiteState WebsiteState,
                IIconTable IconTable, IIconHistoryTable IconHistoryTable,
                IFileReceiver FileReceiver, IIconService IconService) : base(ActiveContext) {
            this.WebsiteState = WebsiteState;
            this.IconTable = IconTable;
            this.IconHistoryTable = IconHistoryTable;
            this.FileReceiver = FileReceiver;
            this.IconService = IconService;
        }

        public void Process(IconPost model) {
            this.NeedNotNull(model, "uploaded icon");
            IPostedFile file = FileReceiver.GetPostedFiles().FirstOrDefault();
            Icon icon = BuildIconFromMessage(model, file);
            IconService.ValidateIconPost(icon, file);

            using (ActiveContext.Start()) {
                CheckAgainstExistingIcons(icon);
                Icon newIcon = UpdateDatabase(icon);
                SaveFile(file, newIcon);
            }
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

        private void CheckAgainstExistingIcons(Icon icon) {
            Icon existing = IconTable.GetByName(icon.Name);
            if (existing != null && existing.Id != icon.Id) {
                throw new PortalException(string.Format("Icon Name '{0}' already exists", icon.Name));
            }
            if (icon.IsNew == false) {
                existing = IconTable.GetById(icon.Id);
                if (existing == null) {
                    throw new PortalException(string.Format("Icon with Id {0} does not exist", icon.Id));
                }
                icon.Image = icon.Image ?? existing.Image; // make sure history has image
            }
        }

        private Icon UpdateDatabase(Icon icon) {
            icon.DateChanged = DateTime.Now;
            if (icon.IsNew) {
                icon.DateCreated = DateTime.Now;
                IconTable.Insert(icon);
            } else {
                IconTable.Update(icon);
            }
            IconTable.Commit();

            Icon newIcon = IconTable.GetByName(icon.Name);
            if (newIcon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found after added", icon.Name));
            }

            IconHistory history = newIcon.ToHistory();
            IconHistoryTable.Insert(history);

            return newIcon;
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
