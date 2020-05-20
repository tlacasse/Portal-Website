using Portal.Data;

namespace Portal.Structure {

    public class CommonDependent {

        protected IConnectionFactory ConnectionFactory { get; }

        public CommonDependent(IConnectionFactory ConnectionFactory) {
            this.ConnectionFactory = ConnectionFactory;
        }

    }

}
