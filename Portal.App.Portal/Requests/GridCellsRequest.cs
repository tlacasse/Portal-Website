using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Structure;
using Portal.Structure.Requests;
using System.Collections.Generic;

namespace Portal.App.Portal.Requests {

    public class GridCellsRequest : CommonDependent, IRequestOut<IEnumerable<IconPosition>> {

        public GridCellsRequest(IConnectionFactory ConnectionFactory) : base(ConnectionFactory) {
        }

        public IEnumerable<IconPosition> Process() {
            using (IConnection connection = ConnectionFactory.Create()) {
                return connection.ActiveGridIcons().ForceLoad();
            }
        }

    }

}
