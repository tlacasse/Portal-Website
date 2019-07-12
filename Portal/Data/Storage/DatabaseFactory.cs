using Portal.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Portal.Data.Storage {

    public class DatabaseFactory : IDatabaseFactory {

        protected IConnectionFactory ConnectionFactory { get; }

        protected IReadOnlyDictionary<Type, TableConfig> TableMap { get; }

        public DatabaseFactory(IConnectionFactory connectionFactory,
                IReadOnlyDictionary<Type, TableConfig> tableMap) {
            this.ConnectionFactory = connectionFactory;
            this.TableMap = tableMap;
        }

        public IDatabase Create() {
            return new Database(TableMap, ConnectionFactory.Create());
        }

    }

}
