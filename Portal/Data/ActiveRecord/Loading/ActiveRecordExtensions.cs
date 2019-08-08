using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Data.ActiveRecord.Loading {

    public static class ActiveRecordExtensions {

        public static string GetTableName(this IActiveRecord record) {
            return record.GetTableAttribute().Name;
        }

        public static TableAttribute GetTableAttribute(this IActiveRecord record) {
            return record.GetType().GetTableAttribute();
        }

        public static ColumnItem GetIdentityColumn(this IActiveRecord record) {
            return record.GetType().GetIdentityColumn();
        }

        public static IEnumerable<ColumnItem> GetColumns(this IActiveRecord record, bool includingIdentity = false) {
            return record.GetType().GetColumns(includingIdentity);
        }

        public static string GetTableName(this Type type) {
            return type.GetTableAttribute().Name;
        }

        public static TableAttribute GetTableAttribute(this Type type) {
            TableAttribute attribute = type.GetCustomAttributes<TableAttribute>().SingleOrDefault();
            if (attribute == null) {
                throw new ActiveRecordLoadingException("TableAttribute not found on " + type.Name);
            }
            return attribute;
        }

        public static ColumnItem GetIdentityColumn(this Type type) {
            foreach (PropertyInfo property in type.GetProperties()) {
                IdentityAttribute identityAttribute = property
                    .GetCustomAttributes<IdentityAttribute>().SingleOrDefault();
                if (identityAttribute != null) {
                    return new ColumnItem(identityAttribute, property);
                }
            }
            throw new ActiveRecordLoadingException("Identity column not found on " + type.Name);
        }

        public static IEnumerable<ColumnItem> GetColumns(this Type type, bool includingIdentity = false) {
            List<ColumnItem> columns = new List<ColumnItem>();
            foreach (PropertyInfo property in type.GetProperties()) {
                ColumnAttribute attribute = property
                    .GetCustomAttributes<ColumnAttribute>(true).SingleOrDefault();
                if (attribute != null) {
                    if (includingIdentity || !(attribute is IdentityAttribute)) {
                        columns.Add(new ColumnItem(attribute, property));
                    }
                }
            }
            return columns;
        }

    }

}
