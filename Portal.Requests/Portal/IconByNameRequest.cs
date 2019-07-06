using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;

namespace Portal.Requests.Portal {

    public class IconByNameRequest : DependentBase, IRequest<string, Icon> {

        public IconByNameRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public Icon Process(string model) {
            string name = PortalUtility.UnUrlFormat(model);
            Icon icon;
            using (IConnection connection = ConnectionFactory.Create()) {
                icon = connection.GetIconByName(name);
            }
            if (icon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found", name));
            }
            return icon;
        }

    }

}
