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
            using (Connection connection = new Connection()) {
                return connection.Execute<Icon>("SELECT * FROM vwPortalIcon");
            }
        }

        /// <summary>
        /// Returns a single Icon, specified by name.
        /// </summary>
        public static Icon GetIconByName(string name, IConnection fromConnection = null) {
            return GetIconByQuery(
                string.Format("SELECT * FROM vwPortalIcon WHERE Name='{0}'", name),
                fromConnection
            );
        }

        /// <summary>
        /// Returns a single Icon, specified by id.
        /// </summary>
        public static Icon GetIconById(int id, IConnection fromConnection = null) {
            return GetIconByQuery(
                string.Format("SELECT * FROM vwPortalIcon WHERE Id={0}", id),
                fromConnection
            );
        }

        private static Icon GetIconByQuery(string query, IConnection fromConnection) {
            IConnection connection = fromConnection ?? new Connection();
            try {
                return connection.Execute<Icon>(query).FirstOrDefault();
            } finally {
                if (fromConnection == null && connection != null) {
                    connection.Dispose();
                }
            }
        }

    }

}
