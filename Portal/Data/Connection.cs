using Portal.Data.Models.Banking;
using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Portal.Data {

    public class Connection : DbContext, IConnection {

        public Connection() : base("Portal") {
            //this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<LogRecord> LogRecordTable { get; set; }
        public DbSet<Icon> IconTable { get; set; }
        public DbSet<IconPosition> IconPositionTable { get; set; }
        public DbSet<IconHistory> IconHistoryTable { get; set; }

        public IEnumerable<LogRecord> LogRecordQuery {
            get { return LogRecordTable; }
        }

        public IEnumerable<Icon> IconQuery {
            get { return IconTable; }
        }

        public IEnumerable<IconPosition> IconPositionQuery {
            get { return IconPositionTable; }
        }

        public IEnumerable<IconHistory> IconHistoryQuery {
            get { return IconHistoryTable; }
        }

        public DbSet<Account> AccountTable { get; set; }
        public DbSet<AccountType> AccountTypeTable { get; set; }
        public DbSet<Category> CategoryTable { get; set; }
        public DbSet<Subcategory> SubcategoryTable { get; set; }

        public IEnumerable<Account> AccountQuery {
            get { return AccountTable; }
        }

        public IEnumerable<AccountType> AccountTypeQuery {
            get { return AccountTypeTable; }
        }

        public IEnumerable<Category> CategoryQuery {
            get { return CategoryTable; }
        }

        public IEnumerable<Subcategory> SubcategoryQuery {
            get { return SubcategoryTable; }
        }

        void IConnection.SaveChanges() {
            this.SaveChanges();
        }

        public void Log(string context, string message) {
            Log(context, message, null);
        }

        public void Log(Exception e) {
            Log("Error", e.Message, e.GetType().ToString());
        }

        private void Log(string context, string message, string exception) {
            try {
                this.LogRecordTable.Add(new LogRecord() {
                    Date = DateTime.Now,
                    Context = context,
                    Message = message,
                    Exception = exception
                });
                this.SaveChanges();
            } catch (Exception e) {
                throw new LoggingFailedException(e);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }

}
