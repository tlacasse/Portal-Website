using Portal.Data.ActiveRecord;
using Portal.Data.ActiveRecord.Storage;
using System;
using System.Collections.Generic;

namespace Portal.Data.Sqlite {

    public interface IConnection : IDisposable, ILogger {

        IEnumerable<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None) where Model : IActiveRecord;

        int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None);

        bool IsClosed { get; }

        void AddTableToCommit(IAppendTable table);

    }

}
