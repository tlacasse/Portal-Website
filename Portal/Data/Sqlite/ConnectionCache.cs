
namespace Portal.Data.Sqlite {

    public class ConnectionCache : IConnectionCache {

        public IConnection Instance {
            get {
                if (ShouldInstaniate()) {
                    instance = new Connection(ConnectionString);
                }
                return instance;
            }
        }

        public string ConnectionString { get; }

        private IConnection instance;

        public ConnectionCache(string ConnectionString) {
            this.ConnectionString = ConnectionString;
        }

        private bool ShouldInstaniate() {
            if (instance == null) {
                return true;
            } else {
                return instance.IsClosed;
            }
        }

    }

}
