using Portal.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Portal.Tests.Fakes {

    public class RecordingFakeConnection : IConnection {

        public List<string> Queries { get; set; } = new List<string>();

        public List<string> NonQueries { get; set; } = new List<string>();

        public void Dispose() {
        }

        public IReadOnlyList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None) {
            Queries.Add(query);
            return new Model[0];
        }

        public int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            NonQueries.Add(query);
            return 1;
        }

        public void Log(string context, string message) {
        }

        public void Log(Exception e) {
        }

    }

}
