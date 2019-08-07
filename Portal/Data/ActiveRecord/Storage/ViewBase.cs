using System.Collections.Generic;
using System.Linq;
using Portal.Data.ActiveRecord.Loading;
using Portal.Data.Querying;
using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public abstract class ViewBase<X> : DatabaseAccessBase, IView<X> where X : IActiveRecord {

        public ViewBase(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public string Name {
            get { return typeof(X).GetTableAttribute().Name; }
        }

        public X GetById(int id) {
            return Query(new Equals<int>("Id", id)).SingleOrDefault();
        }

        public virtual IReadOnlyList<X> Query(IWhere where, QueryOptions queryOptions = QueryOptions.None) {
            string whereClause = where.ToString();
            if (!string.IsNullOrWhiteSpace(whereClause)) {
                whereClause = "WHERE " + whereClause;
            }
            string query = string.Format("SELECT * FROM {0} {1}", Name, whereClause);
            return Connection.Execute<X>(query, queryOptions);
        }

    }

}
