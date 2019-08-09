using Portal.App.Portal.Models;
using Portal.Data.ActiveRecord.Storage;

namespace Portal.App.Portal.Tables {

    public interface IIconTable : ITable<Icon> {

        Icon GetByName(string name);

    }

}
