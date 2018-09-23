using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data {

    /// <summary>
    /// Represents a connection to a single database.
    /// </summary>
    public interface IConnection : IDisposable {

        /// <summary>
        /// Execute a query and return a converted list.
        /// </summary>
        IList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None);

        /// <summary>
        /// Execute a non-query statement and return the number of affected rows.
        /// </summary>
        int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None);

    }

}
