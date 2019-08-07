using Portal.Data.ActiveRecord.Loading;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Data.ActiveRecord.Mapping {

    public static class SqlTranslationExtensions {

        public static string BuildInsertSql(this IActiveRecord record) {
            IEnumerable<ColumnItem> columns = record.GetColumns();
            string tableName = record.GetTableName();
            string columnsList = string.Join(",",
                columns.Select(c => c.Name));
            string valuesList = string.Join(",",
                columns.Select(c => c.GetRecordValue(record)));
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                tableName, columnsList, valuesList);
        }

        public static string BuildUpdateSql(this IActiveRecord record) {
            IEnumerable<ColumnItem> columns = record.GetColumns();
            string tableName = record.GetTableName();
            string equalsList = string.Join(",",
                columns.Select(c => c.GetEqualsExpression(record)));
            string where = record.GetIdentityColumn().GetEqualsExpression(record);
            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                tableName, equalsList, where);
        }

        public static string BuildDeleteSql(this IActiveRecord record) {
            string tableName = record.GetTableName();
            string where = record.GetIdentityColumn().GetEqualsExpression(record);
            return string.Format("DELETE FROM {0} WHERE {1}",
                tableName, where);
        }

    }

}
