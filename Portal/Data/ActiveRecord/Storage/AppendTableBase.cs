using System.Collections.Generic;
using Portal.Data.ActiveRecord.Mapping;
using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public abstract class AppendTableBase<X> : ViewBase<X>, IAppendTable<X> where X : IActiveRecord {

        public virtual int UncommittedChanges => Queries.Count;

        protected virtual List<Query> Queries { get; } = new List<Query>();

        public AppendTableBase(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public virtual int Commit() {
            int sumChanged = 0;
            foreach (Query query in Queries) {
                sumChanged += Connection.ExecuteNonQuery(query.SQL, query.QueryOptions);
            }
            Queries.Clear();
            return sumChanged;
        }

        public virtual void Insert(X item) {
            Queries.Add(new Query(item.BuildInsertSql(), QueryOptions.Log));
            this.Connection.AddTableToCommit(this);
        }

    }

}
