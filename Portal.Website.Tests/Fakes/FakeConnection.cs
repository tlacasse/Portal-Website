using System;
using System.Collections.Generic;
using System.Data.Entity;
using Portal.Data;
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
        }

    }

}