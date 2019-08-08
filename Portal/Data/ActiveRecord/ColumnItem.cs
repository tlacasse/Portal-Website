using Portal.Data.Sqlite;
using System.Collections.Generic;
using System.Reflection;

namespace Portal.Data.ActiveRecord {

    public class ColumnItem {

        public ColumnAttribute ColumnAttribute { get; }

        public PropertyInfo PropertyInfo { get; }

        public string Name => ColumnAttribute.Name;

        public bool IsSimpleMapping => ColumnAttribute.IsSimpleMapping;

        public ColumnItem(ColumnAttribute ColumnAttribute, PropertyInfo PropertyInfo) {
            this.ColumnAttribute = ColumnAttribute;
            this.PropertyInfo = PropertyInfo;
        }

        public string GetRecordValue(IActiveRecord record) {
            return PropertyInfo.GetSqlValue(record);
        }

        public string GetEqualsExpression(IActiveRecord record) {
            string value = GetRecordValue(record);
            return string.Format("{0}={1}", Name, value);
        }

        // generated

        public override bool Equals(object obj) {
            var item = obj as ColumnItem;
            return item != null &&
                   Name == item.Name;
        }

        public override int GetHashCode() {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

    }

}
