using Portal.App.Portal.Models;
using Portal.Data.Storage;
using Portal.Data.Web;
using Portal.Requests;

namespace Portal.App.Portal.Requests {

    public class IconByNameRequest : DependentBase, IRequest<string, Icon> {

        public IconByNameRequest(IWebsiteState WebsiteState, IDatabaseFactory DatabaseFactory)
            : base(WebsiteState, DatabaseFactory) {
        }

        public Icon Process(string model) {
            string name = PortalUtility.UnUrlFormat(model);
            Icon icon;
            using (IDatabase database = DatabaseFactory.Create()) {
                icon = database.GetIconByName(name);
            }
            if (icon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found", name));
            }
            return icon;
        }

    }

}
