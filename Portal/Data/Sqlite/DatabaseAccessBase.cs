
namespace Portal.Data.Sqlite {

    public abstract class DatabaseAccessBase {

        private IConnectionCache ConnectionCache { get; }

        protected IConnection Connection => ConnectionCache.Instance;

        public DatabaseAccessBase(IConnectionCache ConnectionCache) {
            this.ConnectionCache = ConnectionCache;
        }

    }

}
