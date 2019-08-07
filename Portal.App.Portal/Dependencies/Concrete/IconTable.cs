using Portal.App.Portal.Models;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using System.Linq;

namespace Portal.App.Portal.Dependencies.Concrete {

    public class IconTable : TableBase<Icon>, IIconTable {

        public IconTable(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public Icon GetByName(string name) {
            return Query(new Equals<string>("Name", name)).SingleOrDefault();
        }

    }

}
