using System;

namespace Portal.Data.ActiveRecord {

    public class ColumnAttribute : Attribute {

        public virtual string Name { get; }

        public ColumnAttribute(string Name) {
            this.Name = Name;
        }

    }

    public class IdentityAttribute : ColumnAttribute {

        public IdentityAttribute() : base("Id") {
        }

    }

    public class ReferenceAttribute : ColumnAttribute {

        public virtual string Table { get; }

        public ReferenceAttribute(string Table) : base(Table + "Id") {
        }

    }

}
