using Portal.App.Portal.Messages;
using Portal.App.Portal.Models;
using Portal.App.Portal.Tables;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Requests;
using Portal.Structure;
using System;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class IconUploadRequest : DependentBase, IRequestIn<IconPost>, IService<IconUploadRequest> {

        public static readonly int MAX_ICON_MB = 10;
        public static readonly string SAVE_PATH_TEMPLATE = "Data/Icons/{0}.{1}";

        private IIconTable IconTable { get; }
        private IconSaveRequest IconSaveRequest { get; }

        public IconUploadRequest(IActiveContext ActiveContext, IIconTable IconTable,
                IconSaveRequest IconSaveRequest) : base(ActiveContext) {
            this.IconTable = IconTable;
            this.IconSaveRequest = IconSaveRequest;
        }

        public void Process(IconPost model) {
            this.NeedNotNull(model, "uploaded icon");
            IPostedFile file = GetFile(model);

            model.ValidateData();

            // Force DB name to be correctly formatted
            model.Name = PortalUtility.UnUrlFormat(PortalUtility.UrlFormat(model.Name));

            CheckFile(file, model);

            using (ActiveContext.Start()) {
                CheckAgainstExistingIcons(database, model);
                IconSaveRequest.Process(new Icon());
            }
        }

        private IPostedFile GetFile(Icon modelToUpdate) {
            IPostedFile file = FileReceiver.GetPostedFiles().FirstOrDefault();
            if (file != null) {
                modelToUpdate.Image = PortalUtility.GetImageExtension(file.ContentType);
            }
            return file;
        }

        private void CheckFile(IPostedFile file, Icon model) {
            if (model.IsNew && file == null) {
                throw new ArgumentNullException("Icon Image File");
            }
            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new ArgumentOutOfRangeException("Image Upload",
                    string.Format("Is too large (limit {0}MB)", MAX_ICON_MB));
            }
        }

        private void CheckAgainstExistingIcons(IDatabase database, Icon model) {
            Icon existing = database.GetIconByName(model.Name);
            if (existing != null && existing.Id != model.Id) {
                throw new PortalException(string.Format("Icon Name '{0}' already exists", model.Name));
            }
            if (model.IsNew == false) {
                existing = database.GetIconById(model.Id);
                if (existing == null) {
                    throw new PortalException(string.Format("Icon with Id {0} does not exist", model.Id));
                }
                model.Image = model.Image ?? existing.Image; // make sure history has image
            }
        }

    }

}
