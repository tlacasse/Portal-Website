using Portal.Data.Sqlite;
using Portal.Data.Web;

namespace Portal.Requests {

    public abstract class DependentBase {

        protected IConnectionFactory ConnectionFactory { get; }
        protected IWebsiteState WebsiteState { get; }

        public DependentBase(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState) {
            this.ConnectionFactory = ConnectionFactory;
            this.WebsiteState = WebsiteState;
        }

    }

}
