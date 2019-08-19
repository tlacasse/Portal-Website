using Portal.Data.ActiveRecord;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Tests.Fakes.Internal {

    public class FakeTableBackend<X> : ITable<X> where X : IActiveRecord {

        public List<X> Records { get; } = new List<X>();
        public List<X> RecordsToInsert { get; } = new List<X>();
        public List<X> RecordsToUpdate { get; } = new List<X>();
        public List<X> RecordsToDelete { get; } = new List<X>();
        public List<string> NonQueries { get; } = new List<string>();

        public int UncommittedChanges => CountChanges();

        public string Name => "Test Name";

        public void Insert(X item) {
            RecordsToInsert.Add(item);
        }

        public void Delete(X item) {
            RecordsToDelete.Add(item);
        }

        public void Update(X item) {
            RecordsToUpdate.Add(item);
        }

        public void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            NonQueries.Add(query);
        }

        public int Commit() {
            int count = CountChanges();
            IEnumerable<X> leftOverAfterDelete = Records.Where(x => WillBeKeptInRecords(x));
            Records.Clear();
            Records.AddRange(leftOverAfterDelete);
            Records.AddRange(RecordsToUpdate);
            return count;
        }

        public IEnumerable<X> Query(IWhere where, QueryOptions queryOptions = QueryOptions.None) {
            return Records.Where(where);
        }

        public X GetById(int id) {
            throw new NotImplementedException();
        }

        private int CountChanges() {
            return RecordsToInsert.Count + RecordsToUpdate.Count + RecordsToDelete.Count + NonQueries.Count;
        }

        private bool WillBeKeptInRecords(X x) {
            return RecordsToDelete.NotAny(y => x.IsRecordEqual(y))
                && RecordsToUpdate.NotAny(y => x.IsRecordEqual(y));
        }

    }

}
