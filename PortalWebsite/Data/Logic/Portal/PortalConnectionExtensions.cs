using Portal;
using Portal.Data;
using Portal.Models.Portal;
using PortalWebsite.Controllers.Portal;
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

        /// <summary>
        /// Returns a list of all active Grid cells with Icons.
        /// </summary>
        public static IList<IconPosition> GetGridCells(this IConnection connection) {
            return connection.Execute<IconPosition>("SELECT * FROM vwPortalGrid");
        }

        /// <summary>
        /// Returns a Model of the current Grid State.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static GridState GetCurrentGridState(this IConnection connection) {
            GridState current = new GridState() { Size = GridController.CurrentGridSize };
            current.Cells = connection.GetGridCells();
            return current;
        }

        /// <summary>
        /// Returns the history of Grid/Icon changes since the last Grid build.
        /// </summary>
        public static IList<GridChangeItem> GetGridChanges(this IConnection connection) {
            string query =
                string.Format("SELECT * FROM vwPortalGridChanges WHERE DateTime > '{0}'",
                    PortalUtility.DateTimeToSqlLiteString(BuildController.LastBuildTime));
            return connection.Execute<GridChangeItem>(query);
        }

    }

}
