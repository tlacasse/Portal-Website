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
    public class Connection : IDisposable {

        private static readonly string CONNECTION_STRING =
            "Data Source=" + Path.Combine(PortalUtility.SitePath, "Portal\\PortalWebsite.db") + ";Version=3;Password=portal;";

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
        public IList<Model> Execute<Model>(string query) {
            PropertyInfo[] properties = typeof(Model).GetProperties();
            List<Model> results = new List<Model>();
            using (SQLiteCommand command = new SQLiteCommand(query, SQLite)) {
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Model model = PortalUtility.ConstructEmpty<Model>();
                        for (int i = 0; i < reader.FieldCount; i++) {
                            PropertyInfo property = properties.Where(p => p.Name == reader.GetName(i)).First();
                            if (property.PropertyType.Equals(typeof(int)))
                                property.SetValue(model, reader.GetInt32(i));
                            if (property.PropertyType.Equals(typeof(string)))
                                property.SetValue(model, reader.GetString(i));
                            if (property.PropertyType.Equals(typeof(DateTime)))
                                property.SetValue(model, reader.GetDateTime(i));
                        }
                        results.Add(model);
                    }
                }
            }
            return results;
        }

    }

}
