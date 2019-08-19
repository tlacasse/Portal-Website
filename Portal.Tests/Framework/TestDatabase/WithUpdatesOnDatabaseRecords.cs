using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Data.Storage;
using Portal.Tests.Fakes;
using Portal.Tests.Framework.Helpers;

namespace Portal.Tests.Framework.TestDatabase {

    [TestClass]
    public class WithUpdatesOnDatabaseRecords {

        private FakeConnection connection;
        private Database database;

        [TestInitialize]
        public void Setup() {
            connection = new FakeConnection();
            database = DatabaseHelper.GetRealDatabaseWithFakeConnection(connection);
        }

        [TestMethod]
        public void AddedRecordQuery() {
            TestObject added = new TestObject() {
                Name = "New",
                Switch = true
            };
            database.Insert(added);
            database.Commit();
            string query = "INSERT INTO TestTable (Name,Switch) VALUES ('New',1)";
            Assert.AreEqual(query, connection.NonQueries.Single());
        }

        [TestMethod]
        public void DeletedRecordQuery() {
            TestObject added = new TestObject() {
                Id = 2,
                Name = "Old",
                Switch = true
            };
            database.Delete(added);
            database.Commit();
            string query = "DELETE FROM TestTable WHERE Id=2";
            Assert.AreEqual(query, connection.NonQueries.Single());
        }

        [TestMethod]
        public void UpdatedRecordQuery() {
            TestObject added = new TestObject() {
                Id = 2,
                Name = "Old",
                Switch = true
            };
            database.Update(added);
            database.Commit();
            string query = "UPDATE TestTable SET Name='Old',Switch=1 WHERE Id=2";
            Assert.AreEqual(query, connection.NonQueries.Single());
        }

    }


}
