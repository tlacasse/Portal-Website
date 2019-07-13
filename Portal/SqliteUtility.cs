using System;
using System.Reflection;

namespace Portal {

    public static class SqliteUtility {

        public static string SqlTimestamp {
            get { return "datetime(CURRENT_TIMESTAMP, 'localtime')"; }
        }

        public static string ToSqlString(this DateTime dateTime) {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetSqlValue(this PropertyInfo property, object obj) {
            return ConvertToSqlValue(property.GetValue(obj));
        }

        public static string ConvertToSqlValue(object value) {
            if (value == null) {
                return "NULL";
            }
            if (value is string) {
                return "'" + value.ToString() + "'";
            }
            if (value is bool) {
                return ((bool)value) ? "1" : "0";
            }
            if (value is DateTime) {
                return "'" + ((DateTime)value).ToSqlString() + "'";
            }
            return value.ToString();
        }

    }

}
