using System.Reflection;

namespace Portal.Data.Querying {

    public sealed class Equals<T> : IWhere {

        private string FieldName { get; }
        private T Value { get; }

        public Equals(string FieldName, T Value) {
            this.NeedNotNull(FieldName);
            this.NeedNotNull(Value);
            this.FieldName = FieldName;
            this.Value = Value;
        }

        public override string ToString() {
            return FieldName + "=" + SqliteUtility.ConvertToSqlValue(Value);
        }

        public bool Validate(object obj) {
            PropertyInfo propToTest = obj.GetType().GetProperty(FieldName);
            T test = (T)propToTest.GetValue(obj);
            return Value.Equals(test);
        }

    }

}
