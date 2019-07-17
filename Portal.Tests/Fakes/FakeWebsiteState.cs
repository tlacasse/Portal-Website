using Portal.Data.Web;

namespace Portal.Tests.Fakes {

    public class FakeWebsiteState : IWebsiteState {

        public string WebsitePath => "website";

        public string GetPath(string relativePath) {
            return WebsitePath + "/" + relativePath;
        }

    }

}
