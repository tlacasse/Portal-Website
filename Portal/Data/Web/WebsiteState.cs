using Newtonsoft.Json;
using Portal.Models.Portal;
using System;
using System.IO;
using System.Web;

namespace Portal.Data.Web {

    public class WebsiteState : IWebsiteState {

        public string WebsitePath => HttpContext.Current.Server.MapPath("~");

        public string CurrentGridSizePath => Path.Combine(WebsitePath, "Portal/grid.json");

        public string LastGridBuildTimePath => Path.Combine(WebsitePath, "Portal/lastbuildtime.txt");

        public GridSize CurrentGridSize {
            get { return _currentGridSize; }
            set {
                _currentGridSize = value;
                string json = JsonConvert.SerializeObject(_currentGridSize);
                File.WriteAllText(CurrentGridSizePath, json);
            }
        }

        private GridSize _currentGridSize;

        public DateTime LastGridBuildTime {
            get { return _lastGridBuildTime; }
            set {
                _lastGridBuildTime = value;
                File.WriteAllText(LastGridBuildTimePath, _lastGridBuildTime.ToString());
            }
        }

        private DateTime _lastGridBuildTime;

        public string IndexPath => Path.Combine(WebsitePath, "Views/App/Index.cshtml");

        public string IndexEmptyPath => Path.Combine(WebsitePath, "Views/App/IndexEmpty.cshtml");

    }

}
