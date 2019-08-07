using System;
using System.Collections.Generic;

namespace Portal.Data.Sqlite {

    public interface IConnection : IDisposable, ILogger {

        IReadOnlyList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None);

        int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None);

        bool IsClosed { get; }

    }

}
