using Portal.App.Portal.Models;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using Portal.Structure;
using System.Linq;

namespace Portal.App.Portal.Tables {

    public class IconTable : TableBase<Icon>, IIconTable, IService<IIconTable> {

        public IconTable(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public Icon GetByName(string name) {
            return Query(new Equals<string>("Name", name)).SingleOrDefault();
        }

    }

}
