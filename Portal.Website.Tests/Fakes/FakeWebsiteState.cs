using System;
using Portal.Data.Web;
using Portal.Messages;

namespace Portal.Website.Tests.Fakes {

    public class FakeWebsiteState : IWebsiteState {

        public string WebsitePath => "website";

        public string GetPath(string relativePath) {
            return WebsitePath + "/" + relativePath;
        }

        public string IconGridSizePath => throw new NotImplementedException();

        public GridSize ActiveIconGridSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string LastGridBuildTimePath => throw new NotImplementedException();

        public DateTime LastGridBuildTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string IndexEmptyPath => throw new NotImplementedException();

        public string IndexBuiltPath => throw new NotImplementedException();

    }

}
