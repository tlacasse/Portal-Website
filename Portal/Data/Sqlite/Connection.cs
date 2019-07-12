using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Portal.Data.Sqlite {

    public class Connection : IConnection {

        private SQLiteConnection SQLite { get; }

        public Connection(string connectionString) {
            SQLite = new SQLiteConnection(connectionString);
            SQLite.Open();
        }

        public void Dispose() {
            if (SQLite != null) SQLite.Dispose();
        }

        public IReadOnlyList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None) {
            if (options.Includes(QueryOptions.Log)) {
                Log("Query", query);
            }
            PropertyInfo[] properties = typeof(Model).GetProperties();
            List<Model> results = new List<Model>();
            using (SQLiteCommand command = new SQLiteCommand(query, SQLite)) {
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Model model = PortalUtility.ConstructEmpty<Model>();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            PropertyInfo property = properties.Where(p => p.Name == reader.GetName(i)).FirstOrDefault();
                            if (property != null) {
                                if (property.PropertyType.Equals(typeof(int)))
                                    property.SetValue(model, reader.GetInt32(i));
                                if (property.PropertyType.Equals(typeof(string)))
                                    property.SetValue(model, reader.GetString(i));
                                if (property.PropertyType.Equals(typeof(bool)))
                                    property.SetValue(model, reader.GetBoolean(i));
                                if (property.PropertyType.Equals(typeof(DateTime)))
                                    property.SetValue(model, reader.GetDateTime(i));
                            }
                        }
                        results.Add(model);
                    }
                }
            }
            return results;
        }

        public int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            if (options.Includes(QueryOptions.Log)) {
                Log("Query", query);
            }
            using (SQLiteCommand command = new SQLiteCommand(query, SQLite)) {
                int affected = command.ExecuteNonQuery();
                if (affected == 0 && options.Includes(QueryOptions.AllowNoUpdatedRows) == false)
                    throw new PortalException("No rows were affected", query);
                return affected;
            }
        }

        public void Log(string context, string message) {
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

    }

}
