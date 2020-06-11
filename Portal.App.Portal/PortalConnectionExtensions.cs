using Portal.App.Portal.Messages;
using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Web;
using System.Collections.Generic;
using System.Linq;

namespace Portal.App.Portal {

    public static class PortalConnectionExtensions {

        public static Icon IconById(this IConnection connection, int id) {
            return connection.IconQuery.Where(x => x.Id == id).SingleOrDefault();
        }

        public static Icon IconByName(this IConnection connection, string name) {
            return connection.IconQuery.Where(x => x.Name == name).SingleOrDefault();
        }

        public static IEnumerable<IconPosition> ActiveGridIcons(this IConnection connection) {
            return connection.IconPositionQuery.Where(x => x.IsActive).ToList();
        }

        public static GridState BuildCurrentGridState(this IConnection connection, IWebsiteState websiteState) {
            GridState oldGrid = new GridState() {
                Size = websiteState.ActiveIconGridSize
            };
            oldGrid.Cells = connection.ActiveGridIcons();
            return oldGrid;
        }

    }

}
