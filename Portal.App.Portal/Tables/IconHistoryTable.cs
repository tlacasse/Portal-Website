using Portal.App.Portal.Models;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Sqlite;
using System;

namespace Portal.App.Portal.Tables {

    public class IconHistoryTable : AppendTableBase<IconHistory>, IIconHistoryTable {

        public IconHistoryTable(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public void InsertHistory(Icon icon) {
            this.Insert(icon.ToHistory());
        }

    }

}
