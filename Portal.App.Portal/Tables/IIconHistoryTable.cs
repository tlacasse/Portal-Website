using Portal.App.Portal.Models;
using Portal.Data.ActiveRecord.Storage;

namespace Portal.App.Portal.Tables {

    public interface IIconHistoryTable : IAppendTable<IconHistory> {

        void InsertHistory(Icon icon);

    }

}
