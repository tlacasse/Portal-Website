using Portal.Data.Web;
using Portal.Messages;

namespace Portal.Website.Tests.Fakes {

    public class FakeWebsiteState : IWebsiteState {

        public string WebsitePath => "website";

        public string GetPath(string relativePath) {
            return WebsitePath + "/" + relativePath;
        }

        public string IconGridSizePath => throw new System.NotImplementedException();

        public GridSize ActiveIconGridSize { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    }

}
