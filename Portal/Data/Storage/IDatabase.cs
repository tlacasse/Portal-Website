using Portal.Data.Models;
using Portal.Data.Sqlite;
using Portal.Data.Querying;
using System;
using System.Collections.Generic;

namespace Portal.Data.Storage {

    public interface IDatabase : IDisposable, ILogger {

        int UncommittedChanges { get; }

        void Update(IModel model);

        void Insert(IModel model);

        void Delete(IModel model);

        IReadOnlyList<M> Query<M>(IWhere where,
            QueryOptions queryOptions = QueryOptions.None) where M : IModel;

        void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None);

        int Commit();

    }

}
