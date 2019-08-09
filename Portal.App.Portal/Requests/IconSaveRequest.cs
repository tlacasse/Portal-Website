using Portal.App.Portal.Models;
using Portal.App.Portal.Tables;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Requests;
using Portal.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.App.Portal.Requests {

    public class IconSaveRequest : DependentBase, IRequestIn<Icon>, IService<IconSaveRequest> {

        private IIconTable IconTable { get; }
        private IFileReceiver FileReceiver { get; }
        private IWebsiteState WebsiteState { get; }

        public IconSaveRequest(IActiveContext ActiveContext, IIconTable IconTable,
                IWebsiteState WebsiteState, IFileReceiver FileReceiver) : base(ActiveContext) {
            this.IconTable = IconTable;
            this.FileReceiver = FileReceiver;
            this.WebsiteState = WebsiteState;
        }

        public void Process(Icon model) {
            using (ActiveContext.Start()) {
                Icon newIcon = UpdateDatabase(model);
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
            IconTable.Insert(history);

            return newIcon;
        }

        private void SaveFile(IPostedFile file, Icon newIcon) {
            if (file != null) {
                file.SaveAs(WebsiteState.GetPath(
                    string.Format(SAVE_PATH_TEMPLATE, newIcon.Id, newIcon.Image)
                ));
            }
        }

    }

}
