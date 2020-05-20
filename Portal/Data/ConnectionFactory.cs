
namespace Portal.Data {

    public class ConnectionFactory : IConnectionFactory {

        public IConnection Create() {
            return new Connection();
        }

    }

}
