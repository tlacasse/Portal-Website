using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Requests.ConnectionExtensions {

    public static class PortalConnectionExtensions {

        public static IList<Icon> GetIconList(this IConnection connection) {
            return connection.Execute<Icon>("SELECT * FROM vwPortalIcon");
        }

        public static Icon GetIconByName(this IConnection connection, string name) {
            string query = string.Format("SELECT * FROM vwPortalIcon WHERE Name='{0}'", name);
            return connection.Execute<Icon>(query).SingleOrDefault();
        }

        public static Icon GetIconById(this IConnection connection, int id) {
            string query = string.Format("SELECT * FROM vwPortalIcon WHERE Id={0}", id);
            return connection.Execute<Icon>(query).SingleOrDefault();
        }

        public static IList<IconPosition> GetGridCells(this IConnection connection) {
            return connection.Execute<IconPosition>("SELECT * FROM vwPortalGrid");
        }

        public static GridState GetCurrentGridState(this IConnection connection, IWebsiteState websiteState) {
            GridState current = new GridState() { Size = websiteState.CurrentGridSize };
            current.Cells = connection.GetGridCells();
            return current;
        }

        public static IList<GridChangeItem> GetGridChanges(this IConnection connection, IWebsiteState websiteState) {
            string query =
                string.Format("SELECT * FROM vwPortalGridChanges WHERE DateTime > '{0}'",
                    PortalUtility.DateTimeToSqlLiteString(websiteState.LastGridBuildTime));
            return connection.Execute<GridChangeItem>(query);
        }

    }

}
