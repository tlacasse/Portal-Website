using Portal.App.Portal.Models;
using Portal.Data.Sqlite;
using Portal.Data.Storage;
using System;
using System.Collections.Generic;

namespace Portal.Website {

    public static class DatabaseConfig {

        public static IReadOnlyDictionary<string, IDatabaseFactory> Factories {
            get { return factories; }
        }

        private static readonly Dictionary<string, IDatabaseFactory> factories =
            new Dictionary<string, IDatabaseFactory>();

        public static void RegisterDatabaseFactories(IConnectionFactory connectionFactory) {

            Dictionary<Type, TableConfig> portal = new Dictionary<Type, TableConfig>() {
                [typeof(Icon)] = new TableConfig("PortalIcon", TableAccess.FullReadWrite),
                [typeof(IconHistory)] = new TableConfig("PortalIconHistory", TableAccess.InsertOnly)
            };

            factories["Portal"] = new DatabaseFactory(connectionFactory, portal);
        }

    }

}
