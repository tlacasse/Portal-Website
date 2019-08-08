using System;

namespace Portal.Data.ActiveRecord {

    public class ColumnAttribute : Attribute {

        public virtual string Name { get; }

        public virtual bool IsSimpleMapping { get; } = true;

        public ColumnAttribute(string Name) {
            this.Name = Name;
        }

    }

    public class IdentityAttribute : ColumnAttribute {

        public IdentityAttribute() : base("Id") {
        }

    }

    public class ReferencesAttribute : ColumnAttribute {

        public override bool IsSimpleMapping => false;

        public virtual Type ReferenceType { get; }

        public ReferencesAttribute(Type ReferenceType) : base(ReferenceType.Name + "Id") {
        }

    }

}
