using Newtonsoft.Json;
using Portal.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Testing.Fakes.Data {

    public class FakeConnection<TModel> : IConnection {

        private Func<string, IList<TModel>> QueryResult;
        private Action<string> NonQueryAction;

        public FakeConnection(Func<string, IList<TModel>> QueryResult, Action<string> NonQueryAction) {
            this.QueryResult = QueryResult;
            this.NonQueryAction = NonQueryAction;
        }

        public void Dispose() {
        }

        public IList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None) {
            return QueryResult.Invoke(query).Select(model => JsonConvert.SerializeObject(model))
                .Select(json => JsonConvert.DeserializeObject<Model>(json)).ToList();
        }

        public int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            NonQueryAction.Invoke(query);
            return 1;
        }

        public void Log(string context, string message) {
        }

        public void Log(Exception e) {
        }

    }

}
