using Portal.Data.Models.Portal;
using Portal.Data.Models.Shared;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Portal.Data {

    public class Connection : DbContext, IConnection {

        public Connection() : base("Portal") {
        }

        public DbSet<Icon> Icons { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }

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
                this.LogRecords.Add(new LogRecord() {
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
