using Portal.Data;
using Portal.Data.Models.Banking;
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
            AccountInternal = new FakeDbSet<Account>(this);
            AccountTypeInternal = new FakeDbSet<AccountType>(this);
            CategoryInternal = new FakeDbSet<Category>(this);
            SubcategoryInternal = new FakeDbSet<Subcategory>(this);
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

        public FakeDbSet<Account> AccountInternal { get; set; }
        public FakeDbSet<AccountType> AccountTypeInternal { get; set; }
        public FakeDbSet<Category> CategoryInternal { get; set; }
        public FakeDbSet<Subcategory> SubcategoryInternal { get; set; }

        public DbSet<Account> AccountTable {
            get {
                return AccountInternal;
            }
            set { }
        }

        public DbSet<AccountType> AccountTypeTable {
            get {
                return AccountTypeInternal;
            }
            set { }
        }

        public DbSet<Category> CategoryTable {
            get {
                return CategoryInternal;
            }
            set { }
        }

        public DbSet<Subcategory> SubcategoryTable {
            get {
                return SubcategoryInternal;
            }
            set { }
        }

        public IConnection Create() {
            return new FakeConnection(this);
        }

    }

}
