using System;

namespace Portal.App.Banking.Messages {

    public class ListColumn {

        public string Name { get; set; }

        public Type Lookup { get; set; }

        public ListColumn(string Name) : this(Name, null) {
        }

        public ListColumn(string Name, Type Lookup) {
            this.Name = Name;
            this.Lookup = Lookup;
        }

    }

}
