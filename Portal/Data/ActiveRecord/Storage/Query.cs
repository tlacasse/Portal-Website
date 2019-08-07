using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public class Query {

        public string SQL { get; }

        public QueryOptions QueryOptions { get; }

        public Query(string SQL, QueryOptions QueryOptions) {
            this.SQL = SQL;
            this.QueryOptions = QueryOptions;
        }

    }

}
