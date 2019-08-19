using Portal.Data.ActiveRecord.Mapping;
using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public abstract class TableBase<X> : AppendTableBase<X>, ITable<X> where X : IActiveRecord {

        public TableBase(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public virtual void Delete(X item) {
            Queries.Add(new Query(item.BuildDeleteSql(), QueryOptions.Log));
            this.Connection.AddTableToCommit(this);
        }

        public virtual void Update(X item) {
            Queries.Add(new Query(item.BuildUpdateSql(), QueryOptions.Log));
            this.Connection.AddTableToCommit(this);
        }

        public virtual void ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            Queries.Add(new Query(query, options));
            this.Connection.AddTableToCommit(this);
        }

    }

}
