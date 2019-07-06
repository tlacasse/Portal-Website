using System;
using System.Collections.Generic;

namespace Portal.Data.Sqlite {

    public interface IConnection : IDisposable {

        /// <summary>
        /// Execute a query and return a converted list.
        /// </summary>
        IList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None);

        /// <summary>
        /// Execute a non-query statement and return the number of affected rows.
        /// </summary>
        int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None);

        /// <summary>
        /// Log a message with a context.
        /// </summary>
        void Log(string context, string message);

        /// <summary>
        /// Log an error.
        /// </summary>
        void Log(Exception e);

    }

}
