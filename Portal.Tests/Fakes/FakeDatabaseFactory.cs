using Portal.Data.Storage;
using System.Collections.Generic;

namespace Portal.Tests.Fakes {

    public class FakeDatabaseFactory : IDatabaseFactory {

        public readonly List<FakeDatabase> FakeDatabases = new List<FakeDatabase>();

        public IDatabase Create() {
            FakeDatabase db = FakeDatabases[0];
            FakeDatabases.RemoveAt(0);
            return db;
        }

    }

}
