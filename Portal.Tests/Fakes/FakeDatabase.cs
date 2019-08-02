using Portal.Data.Models;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using Portal.Data.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Tests.Fakes {

    public class FakeDatabase : IDatabase {

        public int UncommittedChanges => throw new NotImplementedException();

        public readonly List<string> NonQueriesExecuted = new List<string>();

        public readonly Dictionary<Type, List<IModel>> WorkingDatabase = new Dictionary<Type, List<IModel>>();
        public readonly Dictionary<Type, List<IModel>> InsertedObjects = new Dictionary<Type, List<IModel>>();
        public readonly Dictionary<Type, List<IModel>> DeletedObjects = new Dictionary<Type, List<IModel>>();
        public readonly Dictionary<Type, List<IModel>> UpdatedObjects = new Dictionary<Type, List<IModel>>();

        public void Dispose() {
        }

        public void Insert(IModel model) {
            EnsureValue(InsertedObjects, model.GetType());
            InsertedObjects[model.GetType()].Add(model);
        }

        public void Delete(IModel model) {
            EnsureValue(DeletedObjects, model.GetType());
            DeletedObjects[model.GetType()].Add(model);
        }

        public void Update(IModel model) {
            EnsureValue(UpdatedObjects, model.GetType());
            UpdatedObjects[model.GetType()].Add(model);
        }

        public int Commit() {
            InsertedObjects.ForEach(pair => {
                EnsureValue(WorkingDatabase, pair.Key);
                pair.Value.ForEach(x => WorkingDatabase[pair.Key].Add(x));
            });
            DeletedObjects.ForEach(pair => {
                EnsureValue(WorkingDatabase, pair.Key);
                pair.Value.ForEach(d => {
                    WorkingDatabase[pair.Key]
                        .Where(x => x.IsRecordEqual(d))
                        .ToList()
                        .ForEach(x => WorkingDatabase[pair.Key].Remove(x));
                });
            });
            UpdatedObjects.ForEach(pair => {
                EnsureValue(WorkingDatabase, pair.Key);
                pair.Value.ForEach(u => {
                    var toRemove = WorkingDatabase[pair.Key]
                        .Where(x => x.IsRecordEqual(u))
                        .Single();
                    WorkingDatabase[pair.Key].Remove(toRemove);
                    WorkingDatabase[pair.Key].Add(u);
                });
            });
            return new[] { InsertedObjects, DeletedObjects, UpdatedObjects }
                .SelectMany(d => d.Values)
                .Select(v => v.Count)
                .Sum();
        }

        public IReadOnlyList<M> Query<M>(IWhere where, QueryOptions queryOptions = QueryOptions.None) where M : IModel {
            EnsureValue(WorkingDatabase, typeof(M));
            return WorkingDatabase[typeof(M)]
                .Where(where)
                .Select(x => (M)x).ToList();
        }

        public void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            NonQueriesExecuted.Add(query);
        }

        public void Log(string context, string message) {
        }

        public void Log(Exception e) {
        }

        private void EnsureValue(Dictionary<Type, List<IModel>> dict, Type key) {
            if (!dict.ContainsKey(key)) {
                dict.Add(key, new List<IModel>());
            }
        }

    }

}
