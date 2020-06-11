using Portal.Data.Web;
using Portal.Structure.Requests;

namespace Portal.App.Portal.Requests {

    public class LastGridBuildTimeRequest : IRequestOut<string> {

        private IWebsiteState WebsiteState { get; set; }

        public LastGridBuildTimeRequest(IWebsiteState WebsiteState) {
            this.WebsiteState = WebsiteState;
        }

        public string Process() {
            return WebsiteState.LastGridBuildTime.ToString();
        }

    }

}
