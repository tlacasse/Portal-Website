using Portal.App.Portal.Models;
using Portal.Data.ActiveRecord.Storage;

namespace Portal.App.Portal.Dependencies {

    public interface IIconTable : ITable<Icon> {

        Icon GetByName(string name);

    }

}
