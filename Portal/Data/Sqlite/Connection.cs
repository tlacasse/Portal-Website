using Portal.Data.ActiveRecord;
using Portal.Data.Sqlite.Internal;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Portal.Data.Sqlite {

    public class Connection : IConnection {

        internal SQLiteConnection SQLite { get; }

        public bool IsClosed { get; private set; } = false;

        public Connection(string connectionString) {
            SQLite = new SQLiteConnection(connectionString);
            SQLite.Open();
        }

        public void Dispose() {
            if (SQLite != null) SQLite.Dispose();
            IsClosed = true;
        }

        public IEnumerable<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None)
                where Model : IActiveRecord {
            return Execute(typeof(Model), query, options).Select(x => (Model)x);
        }

        internal IEnumerable<object> Execute(Type type, string query, QueryOptions options = QueryOptions.None) {
            AssertOpen();
            if (options.Includes(QueryOptions.Log)) {
                Log("Query", query);
            }
            QueryService q = new QueryService(this, type, query, options);
            q.SetupSimpleProperties();
            q.SetupReferenceProperties();
            return q.GetResults();
        }

        public int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            AssertOpen();
            if (options.Includes(QueryOptions.Log)) {
                Log("Query", query);
            }
            using (SQLiteCommand command = new SQLiteCommand(query, SQLite)) {
                int affected = command.ExecuteNonQuery();
                if (affected == 0 && options.Includes(QueryOptions.AllowNoUpdatedRows) == false)
                    throw new NoRowsAffectedException("No rows were affected", query);
                return affected;
            }
        }

        public void Log(string context, string message) {
            AssertOpen();
            Log(context, message, null);
        }

        public void Log(Exception e) {
            Log("Error", e.Message, e.GetType().ToString());
        }

        private void Log(string context, string message, string exception) {
            try {
                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO Log (Date, Context, Message");
                if (exception != null) {
                    query.Append(", Exception");
                }
                query.Append(") VALUES ");
                query.Append(string.Format("({0}, '{1}', '{2}'",
                    SqliteUtility.SqlTimestamp, context.Replace("'", "''"), message.Replace("'", "''")
                ));
                if (exception != null) {
                    query.Append(", '").Append(exception).Append("'");
                }
                query.Append(")");
                using (SQLiteCommand command = new SQLiteCommand(query.ToString(), SQLite)) {
                    command.ExecuteNonQuery();
                }
            } catch (Exception e) {
                throw new LoggingFailedException(e);
            }
        }

        private void AssertOpen() {
            if (IsClosed) throw new InvalidOperationException("Connection is closed.");
        }

    }

}
