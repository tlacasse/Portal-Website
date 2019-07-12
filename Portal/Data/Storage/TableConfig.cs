
namespace Portal.Data.Storage {

    public sealed class TableConfig {

        public string Name { get; set; }

        public bool Write { get; set; }

        public TableConfig(string Name, bool Write) {
            this.Name = Name;
            this.Write = Write;
        }

    }

}
