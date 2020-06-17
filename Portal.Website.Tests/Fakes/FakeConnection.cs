using System;
using System.Collections.Generic;
using System.Data.Entity;
using Portal.Data;
using Portal.Data.Models.Banking;
using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;

namespace Portal.Website.Tests.Fakes {

    public class FakeConnection : IConnection {

        public FakeConnectionFactory FakeConnectionFactory { get; set; }

        public FakeConnection(FakeConnectionFactory FakeConnectionFactory) {
            this.FakeConnectionFactory = FakeConnectionFactory;
        }

        public DbSet<LogRecord> LogRecordTable {
            get {
                return FakeConnectionFactory.LogRecordTable;
            }
            set { }
        }

        public DbSet<Icon> IconTable {
            get {
                return FakeConnectionFactory.IconTable;
            }
            set { }
        }

        public DbSet<IconPosition> IconPositionTable {
            get {
                return FakeConnectionFactory.IconPositionTable;
            }
            set { }
        }

        public DbSet<IconHistory> IconHistoryTable {
            get {
                return FakeConnectionFactory.IconHistoryTable;
            }
            set { }
        }

        public DbSet<Account> AccountTable {
            get {
                return FakeConnectionFactory.AccountTable;
            }
            set { }
        }

        public DbSet<AccountType> AccountTypeTable {
            get {
                return FakeConnectionFactory.AccountTypeTable;
            }
            set { }
        }

        public DbSet<Category> CategoryTable {
            get {
                return FakeConnectionFactory.CategoryTable;
            }
            set { }
        }

        public DbSet<Subcategory> SubcategoryTable {
            get {
                return FakeConnectionFactory.SubcategoryTable;
            }
            set { }
        }

        public IEnumerable<LogRecord> LogRecordQuery {
            get {
                return FakeConnectionFactory.LogRecordInternal.Records;
            }
        }

        public IEnumerable<Icon> IconQuery {
            get {
                return FakeConnectionFactory.IconInternal.Records;
            }
        }

        public IEnumerable<IconPosition> IconPositionQuery {
            get {
                return FakeConnectionFactory.IconPositionInternal.Records;
            }
        }

        public IEnumerable<IconHistory> IconHistoryQuery {
            get {
                return FakeConnectionFactory.IconHistoryInternal.Records;
            }
        }

        public IEnumerable<Account> AccountQuery {
            get {
                return FakeConnectionFactory.AccountInternal.Records;
            }
        }

        public IEnumerable<AccountType> AccountTypeQuery {
            get {
                return FakeConnectionFactory.AccountTypeInternal.Records;
            }
        }

        public IEnumerable<Category> CategoryQuery {
            get {
                return FakeConnectionFactory.CategoryInternal.Records;
            }
        }

        public IEnumerable<Subcategory> SubcategoryQuery {
            get {
                return FakeConnectionFactory.SubcategoryInternal.Records;
            }
        }

        public void Dispose() {
        }

        public void Log(Exception e) {
            FakeConnectionFactory.LogRecordInternal.Add(new LogRecord() {
                Exception = e.ToString()
            });
        }

        public void Log(string context, string message) {
            FakeConnectionFactory.LogRecordInternal.Add(new LogRecord() {
                Context = context,
                Message = message
            });
        }

        public void SaveChanges() {
            FakeConnectionFactory.LogRecordInternal.SaveChanges();
            FakeConnectionFactory.IconInternal.SaveChanges();
            FakeConnectionFactory.IconPositionInternal.SaveChanges();
            FakeConnectionFactory.IconHistoryInternal.SaveChanges();
            FakeConnectionFactory.AccountInternal.SaveChanges();
            FakeConnectionFactory.AccountTypeInternal.SaveChanges();
            FakeConnectionFactory.CategoryInternal.SaveChanges();
            FakeConnectionFactory.SubcategoryInternal.SaveChanges();
        }

    }

}