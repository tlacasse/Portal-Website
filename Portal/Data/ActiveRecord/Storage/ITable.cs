using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public interface ITable : IAppendTable {

        void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None);

    }

    public interface ITable<X> : IAppendTable<X>, ITable where X : IActiveRecord {

        void Delete(X item);

        void Update(X item);

    }

}
