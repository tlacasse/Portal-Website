using Portal.Data.Models;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using Portal.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Tests.Fakes {

    public class FakeDatabase : IDatabase {

        public int UncommittedChanges => throw new NotImplementedException();

        public readonly List<IModel> AddedObjects = new List<IModel>();
        public readonly List<IModel> RemovedObjects = new List<IModel>();
        public readonly List<IModel> UpdatedObjects = new List<IModel>();

        public readonly List<string> NonQueriesExecuted = new List<string>();

        public readonly Dictionary<Type, List<IModel>> WorkingDatabase = new Dictionary<Type, List<IModel>>();

        public void Dispose() {
        }

        public void Insert(IModel model) {
            AddedObjects.Add(model);
        }

        public void Delete(IModel model) {
            RemovedObjects.Add(model);
        }

        public void Update(IModel model) {
            UpdatedObjects.Add(model);
        }

        public int Commit() {
            return 1;
        }

        public IReadOnlyList<M> Query<M>(IWhere where, QueryOptions queryOptions = QueryOptions.None) where M : IModel {
            throw new NotImplementedException();
        }

        public void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            NonQueriesExecuted.Add(query);
        }

        public void Log(string context, string message) {
        }

        public void Log(Exception e) {
        }

    }

}
