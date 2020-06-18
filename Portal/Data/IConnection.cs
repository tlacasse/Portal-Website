using Portal.Data.Models.Banking;
using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Portal.Data {

    public interface IConnection : IDisposable {

        DbSet<LogRecord> LogRecordTable { get; set; }
        DbSet<Icon> IconTable { get; set; }
        DbSet<IconPosition> IconPositionTable { get; set; }
        DbSet<IconHistory> IconHistoryTable { get; set; }

        IEnumerable<LogRecord> LogRecordQuery { get; }
        IEnumerable<Icon> IconQuery { get; }
        IEnumerable<IconPosition> IconPositionQuery { get; }
        IEnumerable<IconHistory> IconHistoryQuery { get; }
        /*
        DbSet<Account> AccountTable { get; set; }
        DbSet<AccountType> AccountTypeTable { get; set; }
        DbSet<Category> CategoryTable { get; set; }
        DbSet<Subcategory> SubcategoryTable { get; set; }

        IEnumerable<Account> AccountQuery { get; }
        IEnumerable<AccountType> AccountTypeQuery { get; }
        IEnumerable<Category> CategoryQuery { get; }
        IEnumerable<Subcategory> SubcategoryQuery { get; }
        */
        void SaveChanges();

        void Log(Exception e);

        void Log(string context, string message);

    }

}
