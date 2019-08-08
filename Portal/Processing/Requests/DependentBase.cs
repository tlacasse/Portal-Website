using Portal.Data.Storage;
using Portal.Data.Web;

namespace Portal.Requests {

    public abstract class DependentBase {

        protected IWebsiteState WebsiteState { get; }

        protected IDatabaseFactory DatabaseFactory { get; }

        public DependentBase(IWebsiteState WebsiteState, IDatabaseFactory DatabaseFactory) {
            this.WebsiteState = WebsiteState;
            this.DatabaseFactory = DatabaseFactory;
        }

    }

}
