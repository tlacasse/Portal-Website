using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System;
using System.Data.Entity;

namespace Portal.Data {

    public interface IConnection : IDisposable {

        DbSet<LogRecord> LogRecords { get; set; }
        DbSet<Icon> Icons { get; set; }

        void SaveChanges();

        void Log(Exception e);

        void Log(string context, string message);

    }

}
