using Portal.Data;
using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalWebsite.Data.Logic.Portal {

    public static class PortalConnectionExtensions {

        /// <summary>
        /// Returns a list of all Icons.
        /// </summary>
        public static IList<Icon> GetIconList(this IConnection connection) {
            return connection.Execute<Icon>("SELECT * FROM vwPortalIcon");
        }

        /// <summary>
        /// Returns a single Icon, specified by name.
        /// </summary>
        public static Icon GetIconByName(this IConnection connection, string name) {
            string query = string.Format("SELECT * FROM vwPortalIcon WHERE Name='{0}'", name);
            return connection.Execute<Icon>(query).FirstOrDefault();
        }

        /// <summary>
        /// Returns a single Icon, specified by id.
        /// </summary>
        public static Icon GetIconById(this IConnection connection, int id) {
            string query = string.Format("SELECT * FROM vwPortalIcon WHERE Id={0}", id);
            return connection.Execute<Icon>(query).FirstOrDefault();
        }

    }

}
