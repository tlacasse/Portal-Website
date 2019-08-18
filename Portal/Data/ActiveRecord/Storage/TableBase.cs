using Portal.Data.ActiveRecord.Mapping;
using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public abstract class TableBase<X> : AppendTableBase<X>, ITable<X> where X : IActiveRecord {

        public TableBase(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public void Delete(X item) {
            Queries.Add(new Query(item.BuildDeleteSql(), QueryOptions.Log));
        }

        public void Update(X item) {
            Queries.Add(new Query(item.BuildUpdateSql(), QueryOptions.Log));
        }

        public void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            Queries.Add(new Query(query, options));
        }

    }

}
