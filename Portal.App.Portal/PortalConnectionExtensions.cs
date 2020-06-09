using Portal.Data;
using Portal.Data.Models.Portal;
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

    }

}
