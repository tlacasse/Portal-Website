
namespace Portal.Data.Storage {

    public sealed class TableConfig {

        public string Name { get; }

        public TableAccess AllowedUpdates { get; }

        public TableConfig(string Name, TableAccess AllowedUpdates) {
            this.Name = Name;
            this.AllowedUpdates = AllowedUpdates;
        }

    }

}
