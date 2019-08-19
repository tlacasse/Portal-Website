using System.Collections.Generic;
using System.Linq;
using Portal.Data.ActiveRecord.Loading;
using Portal.Data.Querying;
using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public abstract class ViewBase<X> : DatabaseAccessBase, IView<X> where X : IActiveRecord {

        public ViewBase(IConnectionCache ConnectionCache) : base(ConnectionCache) {
        }

        public virtual string Name {
            get { return typeof(X).GetTableAttribute().Name; }
        }

        public virtual X GetById(int id) {
            return Query(new Equals<int>("Id", id)).SingleOrDefault();
        }

        public virtual IEnumerable<X> Query(IWhere where, QueryOptions queryOptions = QueryOptions.None) {
            string whereClause = where.ToString();
            if (!string.IsNullOrWhiteSpace(whereClause)) {
                whereClause = "WHERE " + whereClause;
            }
            string query = string.Format("SELECT * FROM {0} {1}", Name, whereClause);
            return Connection.Execute<X>(query, queryOptions);
        }

        // generated

        public override bool Equals(object obj) {
            var other = obj as IView;
            return other != null && Name == other.Name;
        }

        public override int GetHashCode() {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

    }

}
