using System;
using Portal.Structure;

namespace Portal.Data.Sqlite {

    public class ConnectionCache : IConnectionCache, IService<IConnectionCache> {

        public IConnection Instance {
            get {
                if (ShouldInstaniate()) {
                    instance = new Connection(ConnectionString);
                }
                return instance;
            }
        }

        public string ConnectionString { get; }

        private Connection instance;

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

        public void AddDisposeHook(Action action) {
            if (!ShouldInstaniate()) {
                instance.AddDisposeHook(action);
            }
        }

    }

}
