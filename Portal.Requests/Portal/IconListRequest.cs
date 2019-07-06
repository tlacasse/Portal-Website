using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;
using System.Collections.Generic;

namespace Portal.Requests.Portal {

    public class IconListRequest : DependentBase, IRequest<Void, IList<Icon>> {

        public IconListRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public IList<Icon> Process(Void model) {
            using (IConnection connection = ConnectionFactory.Create()) {
                return connection.GetIconList();
            }
        }

    }

}
