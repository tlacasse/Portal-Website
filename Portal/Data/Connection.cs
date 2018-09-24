using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data {

    /// <summary>
    /// Wrapper for a SQLite Connection for the Website Database specifically.
    /// </summary>
    public class Connection : IConnection {

        /// <summary>
        /// SQLite connection string.
        /// </summary>
        private static readonly string CONNECTION_STRING =
            "Data Source=" + Path.Combine(PortalUtility.SitePath, "Portal\\PortalWebsite.db") + ";Version=3;Password=portal;";

        /// <summary>
        /// SQLite connection.
        /// </summary>
        private SQLiteConnection SQLite { get; }

        /// <summary>
        /// Open a connection to the Website Database.
        /// </summary>
        public Connection() {
            SQLite = new SQLiteConnection(CONNECTION_STRING);
            SQLite.Open();
        }

        /// <summary>
        /// Close the connection.
        /// </summary>
        public void Dispose() {
            if (SQLite != null) SQLite.Dispose();
        }

        /// <summary>
        /// Executes a query and returns the results converted to the type specified by the generic type parameter.
        /// </summary>
        public IList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None) {
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

        /// <summary>
        /// Executes a SQL statement, such as an UPDATE or INSERT INTO. Throws an exception if no rows were affected.
        /// </summary>
        public int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            if (options.Includes(QueryOptions.Log)) {
                Log("Query", query);
            }
            using (SQLiteCommand command = new SQLiteCommand(query, SQLite)) {
                int affected = command.ExecuteNonQuery();
                if (affected == 0)
                    throw new PortalException("No rows were affected", query);
                return affected;
            }
        }

        /// <summary>
        /// Logs a message with a certain context.
        /// </summary>
        public void Log(string context, string message) {
            Log(context, message, null);
        }

        /// <summary>
        /// Logs an error.
        /// </summary>
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
                    PortalUtility.SqlTimestamp, context.Replace("'", "''"), message.Replace("'", "''")
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
