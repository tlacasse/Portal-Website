using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Structure;
using Portal.Structure.Requests;
using System.Collections.Generic;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class IconListRequest : CommonDependent, IRequestOut<IEnumerable<Icon>> {

        public IconListRequest(IConnectionFactory ConnectionFactory) : base(ConnectionFactory) {
        }

        public IEnumerable<Icon> Process() {
            using (IConnection connection = ConnectionFactory.Create()) {
                return connection.Icons.ToList();
            }
        }

    }

}
