using Portal.App.Portal.Models;
using Portal.App.Portal.Tables;
using Portal.Data.ActiveRecord.Storage;
using Portal.Requests;
using Portal.Structure;

namespace Portal.App.Portal.Requests {

    public class IconByNameRequest : DependentBase, IRequest<string, Icon>, IService<IconByNameRequest> {

        private IIconTable IconTable { get; }

        public IconByNameRequest(IActiveContext ActiveContext, IIconTable IconTable) : base(ActiveContext) {
            this.IconTable = IconTable;
        }

        public Icon Process(string model) {
            this.NeedNotNull(model, "icon name");
            string name = PortalUtility.UnUrlFormat(model);
            Icon icon;
            using (ActiveContext.Start()) {
                icon = IconTable.GetByName(name);
            }
            if (icon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found", name));
            }
            return icon;
        }

    }

}
