using Portal.Data.Storage;
using Portal.Tests.Fakes;
using System;
using System.Collections.Generic;

namespace Portal.Tests.Framework.Helpers {

    public static class DatabaseHelper {

        public static Database GetRealDatabaseWithFakeConnection(FakeConnection connection) {
            Dictionary<Type, TableConfig> d = new Dictionary<Type, TableConfig>() {
                [typeof(TestObject)] = new TableConfig("TestTable", TableAccess.FullReadWrite)
            };
            return new Database(d, connection);
        }

    }

}
