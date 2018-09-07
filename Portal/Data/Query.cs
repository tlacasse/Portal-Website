using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data {

    /// <summary>
    /// Some standard queries to the Website Database.
    /// </summary>
    public static class Query {

        /// <summary>
        /// Returns a list of all Icons.
        /// </summary>
        public static IList<Icon> GetIconList() {
            IList<Icon> results;
            using (Connection connection = new Connection()) {
                results = connection.Execute<Icon>("SELECT * FROM PortalIcon");
            }
            return results;
        }

    }

}
