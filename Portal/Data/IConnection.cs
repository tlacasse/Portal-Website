using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Portal.Data {

    public interface IConnection : IDisposable {

        DbSet<LogRecord> LogRecords { get; set; }
        DbSet<Icon> Icons { get; set; }
        DbSet<IconHistory> IconHistories { get; set; }

        IEnumerable<LogRecord> LogRecordQuery { get; }
        IEnumerable<Icon> IconQuery { get; }
        IEnumerable<IconHistory> IconHistoryQuery { get; }

        void SaveChanges();

        void Log(Exception e);

        void Log(string context, string message);

    }

}
