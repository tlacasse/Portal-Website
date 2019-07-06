using Portal.Data.Web;
using Portal.Models.Portal;
using System;

namespace Portal.Testing.Fakes.Data {

    public class FakeWebsiteState : IWebsiteState {

        public string WebsitePath => string.Empty;

        public string CurrentGridSizePath => string.Empty;

        public string LastGridBuildTimePath => string.Empty;

        public GridSize CurrentGridSize {
            get {
                return new GridSize() {
                    Width = 10,
                    Height = 6
                };
            }
            set { }
        }
        public DateTime LastGridBuildTime {
            get {
                return DateTime.MinValue;
            }
            set { }
        }

        public string IndexPath => string.Empty;

        public string IndexEmptyPath => string.Empty;

    }

}
