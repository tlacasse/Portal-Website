using Portal.Data.Querying;
using Portal.Data.Sqlite;
using System.Collections.Generic;

namespace Portal.Data.ActiveRecord.Storage {

    public interface IView {

        string Name { get; }

    }

    public interface IView<X> : IView where X : IActiveRecord {

        X GetById(int id);

        IEnumerable<X> Query(IWhere where, QueryOptions queryOptions = QueryOptions.None);

    }

}
