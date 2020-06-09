using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System.Data.Entity;

namespace Portal.Website.Tests.Fakes {

    public class FakeConnectionFactory : IConnectionFactory {

        public FakeConnectionFactory() {
            LogRecordsList = new FakeDbSet<LogRecord>(this);
            IconsList = new FakeDbSet<Icon>(this);
            IconHistoriesList = new FakeDbSet<IconHistory>(this);
        }

        public FakeDbSet<LogRecord> LogRecordsList { get; set; }
        public FakeDbSet<Icon> IconsList { get; set; }
        public FakeDbSet<IconPosition> IconPositionsList { get; set; }
        public FakeDbSet<IconHistory> IconHistoriesList { get; set; }

        public DbSet<LogRecord> LogRecords {
            get {
                return LogRecordsList;
            }
            set { }
        }

        public DbSet<Icon> Icons {
            get {
                return IconsList;
            }
            set { }
        }

        public DbSet<IconPosition> IconPositions {
            get {
                return IconPositionsList;
            }
            set { }
        }

        public DbSet<IconHistory> IconHistories {
            get {
                return IconHistoriesList;
            }
            set { }
        }

        public IConnection Create() {
            return new FakeConnection(this);
        }

    }

}
