using System;

namespace Portal.Data.ActiveRecord {

    public class TableAttribute : Attribute {

        public string Name { get; }

        public TableAttribute(string Name) {
            this.Name = Name;
        }

    }

}
