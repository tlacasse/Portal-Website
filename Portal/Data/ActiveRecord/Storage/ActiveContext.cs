using Portal.Data.Sqlite;

namespace Portal.Data.ActiveRecord.Storage {

    public class ActiveContext : IActiveContext {

        private IConnectionCache ConnectionCache { get; }

        public ActiveContext(IConnectionCache ConnectionCache) {
            this.ConnectionCache = ConnectionCache;
        }

        public IConnection Start() {
            return ConnectionCache.Instance;
        }

        public void Dispose() {
            ConnectionCache.Instance.Dispose();
        }

    }

}
