using Portal.Data.Models;
using Portal.Data.Models.Attributes;
using Portal.Data.Sqlite;
using Portal.Data.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Data.Storage {

    public class Database : IDatabase {

        public int UncommittedChanges => Queries.Count;

        private IList<Tuple<string, QueryOptions>> Queries { get; } =
            new List<Tuple<string, QueryOptions>>();

        private IReadOnlyDictionary<Type, TableConfig> TableMap { get; }

        private IConnection Connection { get; }

        public Database(IReadOnlyDictionary<Type, TableConfig> tableMap, IConnection connection) {
            this.Connection = connection;
            this.TableMap = tableMap;
        }

        public void Dispose() {
            Commit();
            Connection.Dispose();
        }

        public void Update(IModel model) {
            this.NeedNotNull(model);
            TableConfig table = GetTable(model.GetType());
            string tableName = table.Name;
            if (table.AllowedUpdates < TableAccess.FullReadWrite) {
                throw new InvalidOperationException(
                    string.Format("Can not update records in '{0}'", tableName));
            }

            IEnumerable<PropertyInfo> properties = GetProperties(model);
            string equalsList = string.Join(",",
                properties.Select(p => p.Name + "=" + p.GetSqlValue(model)));
            PropertyInfo idProperty = GetIdProperty(model);
            string idValue = idProperty.GetSqlValue(model);

            string query = string.Format("UPDATE {0} SET {1} WHERE {2}={3}",
                tableName, equalsList, idProperty.Name, idValue);
            Queries.Add(new Tuple<string, QueryOptions>(query, QueryOptions.Log));
        }

        public void Insert(IModel model) {
            this.NeedNotNull(model);
            TableConfig table = GetTable(model.GetType());
            string tableName = table.Name;
            if (table.AllowedUpdates < TableAccess.InsertOnly) {
                throw new InvalidOperationException(
                    string.Format("Can not add records to '{0}'", tableName));
            }

            IEnumerable<PropertyInfo> properties = GetProperties(model);
            string columnsList = string.Join(",", properties.Select(p => p.Name));
            string valuesList = string.Join(",", properties.Select(p => p.GetSqlValue(model)));

            string query = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                tableName, columnsList, valuesList);
            Queries.Add(new Tuple<string, QueryOptions>(query, QueryOptions.Log));
        }

        public void Delete(IModel model) {
            this.NeedNotNull(model);
            TableConfig table = GetTable(model.GetType());
            string tableName = table.Name;
            if (table.AllowedUpdates < TableAccess.FullReadWrite) {
                throw new InvalidOperationException(
                    string.Format("Can not remove records in '{0}'", tableName));
            }

            PropertyInfo idProperty = GetIdProperty(model);
            string idValue = idProperty.GetSqlValue(model);

            string query = string.Format("DELETE FROM {0} WHERE {1}={2}",
                tableName, idProperty.Name, idValue);
            Queries.Add(new Tuple<string, QueryOptions>(query, QueryOptions.Log));
        }

        public IReadOnlyList<M> Query<M>(IWhere where, QueryOptions queryOptions = QueryOptions.None) where M : IModel {
            string whereClause = where.ToString();
            if (!string.IsNullOrWhiteSpace(whereClause)) {
                whereClause = "WHERE " + whereClause;
            }
            TableConfig table = GetTable(typeof(M));
            string tableName = table.Name;
            string query = string.Format("SELECT * FROM {0} {1}", tableName, whereClause);
            return Connection.Execute<M>(query, queryOptions);
        }

        public int Commit() {
            int sumChanged = 0;
            foreach (Tuple<string, QueryOptions> query in Queries) {
                sumChanged += Connection.ExecuteNonQuery(query.Item1, query.Item2);
            }
            Queries.Clear();
            return sumChanged;
        }

        public void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            Queries.Add(new Tuple<string, QueryOptions>(query, options));
        }

        public void Log(string context, string message) {
            Connection.Log(context, message);
        }

        public void Log(Exception e) {
            Connection.Log(e);
        }

        private TableConfig GetTable(Type type) {
            if (TableMap.ContainsKey(type) == false) {
                throw new ArgumentException(type + " table name is not available.");
            }
            return TableMap[type];
        }

        private PropertyInfo GetIdProperty(object obj) {
            Type objtype = obj.GetType();
            return obj.GetType()
                .GetProperties()
                .Where(p => p.HasAttribute<IdentityAttribute>())
                .Single();
        }

        private IEnumerable<PropertyInfo> GetProperties(object obj, bool includeId = false) {
            IEnumerable<PropertyInfo> properties =
                obj.GetType().GetProperties()
                .Where(p => !p.HasAttribute<UpdateIgnoreAttribute>());
            if (includeId == false) {
                properties = properties
                    .Where(p => !p.HasAttribute<IdentityAttribute>());
            }
            return properties;
        }

    }

}
