using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Data.Querying;
using Portal.Data.Storage;
using Portal.Tests.Fakes;
using Portal.Tests.Framework.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Tests.Framework.TestQuerying {

    [TestClass]
    public class WithQueryOnDatabase {

        private Dictionary<string, IWhere> queries;

        private RecordingFakeConnection connection;
        private Database database;

        [TestInitialize]
        public void Setup() {
            queries = QueryingHelper.GetTestQueries();
            connection = new RecordingFakeConnection();
            database = DatabaseHelper.GetRealDatabaseWithFakeConnection(connection);
        }

        [TestMethod]
        public void DatabaseStringQueryWithWhere() {
            IWhere query = new And(queries["SwitchIsTrue"], new Or(queries["NameEqualsApple"], queries["IdEquals1"]));
            database.Query<TestObject>(query);
            string strQuery = "SELECT * FROM TestTable WHERE (Switch=1 AND (Name='Apple' OR Id=1))";
            Assert.AreEqual(strQuery, connection.Queries.Single());
        }

        [TestMethod]
        public void DatabaseStringQueryWithoutWhere() {
            database.Query<TestObject>(new All());
            string strQuery = "SELECT * FROM TestTable";
            Assert.AreEqual(strQuery.Trim(), connection.Queries.Single().Trim());
        }

    }

}
