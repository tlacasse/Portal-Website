using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;
using System.Collections.Generic;

namespace Portal.Requests.Portal {

    public class GridChangeHistoryRequest : DependentBase, IRequest<Void, IList<GridChangeItem>> {

        public GridChangeHistoryRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public IList<GridChangeItem> Process(Void model) {
            using (IConnection connection = ConnectionFactory.Create()) {
                return connection.GetGridChanges(WebsiteState);
            }
        }

    }

}
