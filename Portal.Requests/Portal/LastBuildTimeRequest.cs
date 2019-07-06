using Portal.Data.Sqlite;
using Portal.Data.Web;

namespace Portal.Requests.Portal {

    public class LastBuildTimeRequest : DependentBase, IRequest<Void, string> {

        public LastBuildTimeRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public string Process(Void model) {
            return WebsiteState.LastGridBuildTime.ToString("F");
        }

    }

}
