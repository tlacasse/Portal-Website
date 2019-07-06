
namespace Portal.Data.Sqlite {

    public class ConnectionFactory : IConnectionFactory {

        public string ConnectionString { get; }

        public ConnectionFactory(string ConnectionString) {
            this.ConnectionString = ConnectionString;
        }

        public IConnection Create() {
            return new Connection(ConnectionString);
        }

    }

}
