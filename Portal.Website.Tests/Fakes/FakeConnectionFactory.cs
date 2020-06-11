using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System.Data.Entity;

namespace Portal.Website.Tests.Fakes {

    public class FakeConnectionFactory : IConnectionFactory {

        public FakeConnectionFactory() {
            LogRecordInternal = new FakeDbSet<LogRecord>(this);
            IconInternal = new FakeDbSet<Icon>(this);
            IconPositionInternal = new FakeDbSet<IconPosition>(this);
            IconHistoryInternal = new FakeDbSet<IconHistory>(this);
        }

        public FakeDbSet<LogRecord> LogRecordInternal { get; set; }
        public FakeDbSet<Icon> IconInternal { get; set; }
        public FakeDbSet<IconPosition> IconPositionInternal { get; set; }
        public FakeDbSet<IconHistory> IconHistoryInternal { get; set; }

        public DbSet<LogRecord> LogRecordTable {
            get {
                return LogRecordInternal;
            }
            set { }
        }

        public DbSet<Icon> IconTable {
            get {
                return IconInternal;
            }
            set { }
        }

        public DbSet<IconPosition> IconPositionTable {
            get {
                return IconPositionInternal;
            }
            set { }
        }

        public DbSet<IconHistory> IconHistoryTable {
            get {
                return IconHistoryInternal;
            }
            set { }
        }

        public IConnection Create() {
            return new FakeConnection(this);
        }

    }

}
