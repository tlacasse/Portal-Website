using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Data.Querying;
using Portal.Data.Storage;
using Portal.Tests.Framework.Helpers;

namespace Portal.Tests.Framework.TestQuerying {

    [TestClass]
    public class QueriesOnEnumerable {

        private static List<TestObject> list;
        private static Dictionary<string, IWhere> queries;

        [ClassInitialize]
        public static void Setup(TestContext _) {
            list = QueryingHelper.GetTestEnumerable();
            queries = QueryingHelper.GetTestQueries();
        }

        [TestMethod]
        public void All() {
            Assert.AreEqual(18, list.Where(new All()).Count());
        }

        [TestMethod]
        public void SingleEquals() {
            IEnumerable<TestObject> results = list.Where(queries["NameEqualsApple"]);
            Assert.AreEqual(6, results.Count());
            Assert.AreEqual(6, results.Where(x => x.Name == "Apple").Count());
        }

        [TestMethod]
        public void And() {
            IWhere and = new And(queries["NameEqualsApple"], queries["IdEquals1"]);
            IEnumerable<TestObject> results = list.Where(and);
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(2, results.Where(x => x.Name == "Apple" && x.Id == 1).Count());
        }

        [TestMethod]
        public void Or() {
            IWhere or = new Or(queries["NameEqualsApple"], queries["IdEquals1"]);
            IEnumerable<TestObject> results = list.Where(or);
            Assert.AreEqual(10, results.Count());
            Assert.AreEqual(10, results.Where(x => x.Name == "Apple" || x.Id == 1).Count());
        }

        [TestMethod]
        public void Layered() {
            IWhere or = new And(queries["SwitchIsTrue"], new Or(queries["NameEqualsApple"], queries["IdEquals1"]));
            IEnumerable<TestObject> results = list.Where(or);
            Assert.AreEqual(5, results.Count());
            Assert.AreEqual(5, results.Where(x => x.Switch && (x.Name == "Apple" || x.Id == 1)).Count());
        }

    }

}
