using Portal.Data;
using Portal.Data.Web;

namespace Portal.Structure {

    public class CommonDependent2 : CommonDependent {

        protected IWebsiteState WebsiteState { get; }

        public CommonDependent2(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState) : base(ConnectionFactory) {
            this.WebsiteState = WebsiteState;
        }

    }

}
