using Portal.App.Portal.Models;
using Portal.Data.Querying;
using Portal.Data.Storage;
using System.Linq;

namespace Portal.App.Portal {

    public static class PortalDatabaseExtensions {

        public static Icon GetIconByName(this IDatabase database, string name) {
            return database.Query<Icon>(new Equals<string>("Name", name)).SingleOrDefault();
        }

        public static Icon GetIconById(this IDatabase database, int id) {
            return database.Query<Icon>(new Equals<int>("Id", id)).SingleOrDefault();
        }

    }

}
