﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data {

    /// <summary>
    /// Quickly build an INSERT INTO or UPDATE query, while being able to dynamically set which one will be generated.
    /// </summary>
    public class DatabaseUpdateQuery {

        /// <summary>
        /// Type of Database Update Query.
        /// </summary>
        public enum QueryType {
            INSERT, UPDATE
        }

        private class Field {
            public string Name { get; set; }
            public IEnumerable<string> Values { get; set; }
            public bool IsQuoted { get; set; }

            public string Quote {
                get { return IsQuoted ? "'" : ""; }
            }
        }

        /// <summary>
        /// Exact WHERE clause to be included in queries, must include 'WHERE'.
        /// </summary>
        public string WhereClause { get; set; }

        /// <summary>
        /// Type of Query
        /// </summary>
        public QueryType Type { get; }

        /// <summary>
        /// Which table the update will affect.
        /// </summary>
        public string Table { get; }

        private List<Field> Fields { get; }

        /// <summary>
        /// Creates the base for a new query.
        /// </summary>
        public DatabaseUpdateQuery(QueryType Type, string Table) {
            this.Type = Type;
            this.Table = Table;
            Fields = new List<Field>();
        }

        /// <summary>
        /// Adds a field to this query.
        /// </summary>
        public void AddField(string Name, string Value, bool IsQuoted = true) {
            Fields.Add(new Field() {
                Name = Name,
                Values = new string[] { Value },
                IsQuoted = IsQuoted
            });
        }

        /// <summary>
        /// Adds a field to this query.
        /// </summary>
        public void AddField(string Name, IEnumerable<string> Value, bool IsQuoted = true) {
            Fields.Add(new Field() {
                Name = Name,
                Values = Value,
                IsQuoted = IsQuoted
            });
        }

        /// <summary>
        /// Generate the SQL.
        /// </summary>
        public string Build() {
            switch (Type) {
                case QueryType.UPDATE: return BuildUpdate();
                case QueryType.INSERT: return BuildInsert();
            }
            return null;
        }

        private string BuildUpdate() {
            if (WhereClause == null) {
                throw new InvalidOperationException("WHERE clause is not set");
            }
            EnsureValuesAllHaveSameNumber();
            if (Fields.FirstOrDefault().Values.Count() != 1) {
                throw new InvalidOperationException("Update statements only allow a single value per field.");
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + Table + " SET ");
            sb.Append(
                string.Join(", ",
                    Fields.Select(f => string.Format("{0}={1}{2}{1}",
                        f.IsQuoted ? f.Name.Replace("'", "''") : f.Name,
                        f.Quote, f.Values.Single()))
                )
            );
            sb.Append(" ");
            sb.Append(WhereClause);
            return sb.ToString();
        }

        private string BuildInsert() {
            EnsureValuesAllHaveSameNumber();
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + Table + " (");
            sb.Append(string.Join(", ", Fields.Select(f => f.Name)));
            sb.Append(") VALUES ");

            IEnumerable<List<string>> inserts = Fields.First().Values.Select(_ => new List<string>());
            foreach (Field field in Fields) {
                inserts = inserts.Zip(field.Values,
                    (list, value) => AddAndReturnList(list, string.Format("{0}{1}{0}", field.Quote,
                        field.IsQuoted ? value.Replace("'", "''") : value
                    ))
                );
            }
            sb.Append(
                string.Join(", ",
                    inserts.Select(list => "(" + string.Join(", ", list) + ")")
                )
            );
            return sb.ToString();
        }

        private void EnsureValuesAllHaveSameNumber() {
            if (Fields.Any()) {
                int count = Fields.First().Values.Count();
                foreach (Field field in Fields) {
                    if (field.Values.Count() != count) {
                        throw new ArgumentOutOfRangeException("Fields", "Field value counts are not all the same.");
                    }
                }
            } else {
                throw new InvalidOperationException("No fields specified.");
            }
        }

        private static List<T> AddAndReturnList<T>(List<T> list, T value) {
            list.Add(value);
            return list;
        }

    }

}
