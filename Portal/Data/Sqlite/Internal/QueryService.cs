using Portal.Data.ActiveRecord;
using Portal.Data.ActiveRecord.Loading;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace Portal.Data.Sqlite.Internal {

    internal class QueryService {

        private Connection Connection { get; }
        private Type ModelType { get; }
        private string Query { get; }
        private QueryOptions QueryOptions { get; }

        private IEnumerable<ColumnItem> Columns { get; }

        private List<ModelItem> Models { get; set; }

        internal QueryService(Connection Connection, Type ModelType, string Query, QueryOptions QueryOptions) {
            this.Connection = Connection;
            this.ModelType = ModelType;
            this.Query = Query;
            this.QueryOptions = QueryOptions;

            Columns = ModelType.GetColumns();
        }

        internal void SetupSimpleProperties() {
            Models = new List<ModelItem>();
            using (SQLiteCommand command = new SQLiteCommand(Query, Connection.SQLite)) {
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Models.Add(MapRecord(reader));
                    }
                }
            }
        }

        internal void SetupReferenceProperties() {
            foreach (ModelItem model in Models) {
                foreach (KeyValuePair<ColumnItem, int> reference in model.References) {
                    Type referenceType = (reference.Key.ColumnAttribute as ReferencesAttribute).ReferenceType;
                    string tableName = referenceType.GetTableName();
                    string query = string.Format("SELECT * FROM {0} WHERE Id = {1}", tableName, reference.Value);
                    object fetchedObject = Connection.Execute(referenceType, query, QueryOptions);
                    reference.Key.PropertyInfo.SetValue(model.Object, fetchedObject);
                }
            }
        }

        internal IEnumerable<object> GetResults() {
            return Models.Select(i => i.Object);
        }

        private ModelItem MapRecord(SQLiteDataReader reader) {
            ModelItem model = new ModelItem() {
                Object = PortalUtility.ConstructEmpty(ModelType)
            };
            for (int i = 0; i < reader.FieldCount; i++) {
                if (!reader.IsDBNull(i)) {
                    string name = reader.GetName(i);
                    ColumnItem column = Columns.Where(c => c.Name == name).SingleOrDefault();
                    if (column == null) {
                        ThrowPropertyNotFound(name);
                    } else {
                        if (column.IsSimpleMapping) {
                            MapProperty(reader, column.PropertyInfo, i, model.Object);
                        } else if (column.ColumnAttribute is ReferencesAttribute) {
                            model.References[column] = reader.GetInt32(i);
                        } else {
                            ThrowPropertyNotFound(name);
                        }
                    }
                }
            }
            return model;
        }

        private void MapProperty(SQLiteDataReader reader, PropertyInfo property, int i, object model) {
            if (property.PropertyType.Equals(typeof(int)))
                property.SetValue(model, reader.GetInt32(i));
            if (property.PropertyType.Equals(typeof(string)))
                property.SetValue(model, reader.GetString(i));
            if (property.PropertyType.Equals(typeof(bool)))
                property.SetValue(model, reader.GetBoolean(i));
            if (property.PropertyType.Equals(typeof(DateTime)))
                property.SetValue(model, reader.GetDateTime(i));
        }

        private void ThrowPropertyNotFound(string name) {
            throw new ArgumentException(string.Format("'{0}' property not found in '{1}'.", name, ModelType.Name));
        }

    }

}
