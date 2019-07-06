using System.Collections.Generic;
using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;

namespace Portal.Requests.Portal {

    public class GridCellsRequest : DependentBase, IRequest<Void, IEnumerable<IconPosition>> {

        public GridCellsRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public IEnumerable<IconPosition> Process(Void model) {
            using (IConnection connection = ConnectionFactory.Create()) {
                return connection.GetGridCells();
            }
        }

    }

}
