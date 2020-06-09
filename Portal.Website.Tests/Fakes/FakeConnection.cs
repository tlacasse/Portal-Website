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

        public DbSet<LogRecord> LogRecords {
            get {
                return FakeConnectionFactory.LogRecords;
            }
            set { }
        }

        public DbSet<Icon> Icons {
            get {
                return FakeConnectionFactory.Icons;
            }
            set { }
        }

        public DbSet<IconPosition> IconPositions {
            get {
                return FakeConnectionFactory.IconPositions;
            }
            set { }
        }

        public DbSet<IconHistory> IconHistories {
            get {
                return FakeConnectionFactory.IconHistories;
            }
            set { }
        }

        public IEnumerable<LogRecord> LogRecordQuery {
            get {
                return FakeConnectionFactory.LogRecordsList.Records;
            }
        }

        public IEnumerable<Icon> IconQuery {
            get {
                return FakeConnectionFactory.IconsList.Records;
            }
        }

        public IEnumerable<IconPosition> IconPositionQuery {
            get {
                return FakeConnectionFactory.IconPositionsList.Records;
            }
        }

        public IEnumerable<IconHistory> IconHistoryQuery {
            get {
                return FakeConnectionFactory.IconHistoriesList.Records;
            }
        }

        public void Dispose() {
        }

        public void Log(Exception e) {
            FakeConnectionFactory.LogRecordsList.Add(new LogRecord() {
                Exception = e.ToString()
            });
        }

        public void Log(string context, string message) {
            FakeConnectionFactory.LogRecordsList.Add(new LogRecord() {
                Context = context,
                Message = message
            });
        }

        public void SaveChanges() {
            FakeConnectionFactory.LogRecordsList.SaveChanges();
            FakeConnectionFactory.IconsList.SaveChanges();
            FakeConnectionFactory.IconHistoriesList.SaveChanges();
        }

    }

}