using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Data;

namespace PortalTesting {

    [TestClass]
    public class TestDatabaseUpdateQuery {

        [TestMethod]
        public void UpdateQuery() {
            DatabaseUpdateQuery updateQuery = new DatabaseUpdateQuery(DatabaseUpdateQuery.QueryType.UPDATE, "TestTable");
            updateQuery.AddField("NumberField", "1", false);
            updateQuery.AddField("StringField", "String");
            updateQuery.WhereClause = "WHERE 1=1";

            StringBuilder expected = new StringBuilder();
            expected.Append("UPDATE TestTable SET ");
            expected.Append("NumberField=1, StringField='String' ");
            expected.Append("WHERE 1=1");

            Assert.AreEqual(expected.ToString(), updateQuery.Build());
        }

        [TestMethod]
        public void InsertQuery_Single() {
            DatabaseUpdateQuery insertQuery = new DatabaseUpdateQuery(DatabaseUpdateQuery.QueryType.INSERT, "TestTable");
            insertQuery.AddField("NumberField", "1", false);
            insertQuery.AddField("StringField", "String");

            StringBuilder expected = new StringBuilder();
            expected.Append("INSERT INTO TestTable (NumberField, StringField) VALUES ");
            expected.Append("(1, 'String')");

            Assert.AreEqual(expected.ToString(), insertQuery.Build());
        }

        [TestMethod]
        public void InsertQuery_Multiple() {
            DatabaseUpdateQuery insertQuery = new DatabaseUpdateQuery(DatabaseUpdateQuery.QueryType.INSERT, "TestTable");
            insertQuery.AddField("NumberField", new string[] { "1", "2", "3" }, false);
            insertQuery.AddField("StringField", new string[] { "Apple", "Banana", "Cranberry" });

            StringBuilder expected = new StringBuilder();
            expected.Append("INSERT INTO TestTable (NumberField, StringField) VALUES ");
            expected.Append("(1, 'Apple')");
            expected.Append(", (2, 'Banana')");
            expected.Append(", (3, 'Cranberry')");

            Assert.AreEqual(expected.ToString(), insertQuery.Build());
        }

    }

}
