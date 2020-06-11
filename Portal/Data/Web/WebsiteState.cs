using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Portal.Messages;

namespace Portal.Data.Web {

    public class WebsiteState : IWebsiteState {

        public string WebsitePath => HttpContext.Current.Server.MapPath("~");

        public string GetPath(string relativePath) {
            return Path.Combine(WebsitePath, relativePath);
        }

        public string IconGridSizePath => Path.Combine(WebsitePath, "App_Data/grid.json");

        public GridSize ActiveIconGridSize {
            get { return currentGridSize; }
            set {
                currentGridSize = value;
                string json = JsonConvert.SerializeObject(currentGridSize);
                File.WriteAllText(IconGridSizePath, json);
            }
        }

        private GridSize currentGridSize;

        public string LastGridBuildTimePath => Path.Combine(WebsitePath, "App_Data/lastgridbuildtime.txt");

        public DateTime LastGridBuildTime {
            get { return lastGridBuildTime; }
            set {
                lastGridBuildTime = value;
                File.WriteAllText(LastGridBuildTimePath, lastGridBuildTime.ToString());
            }
        }

        private DateTime lastGridBuildTime;

        public string IndexEmptyPath => Path.Combine(WebsitePath, "App_Data/IndexEmpty.cshtml");

        public string IndexBuiltPath => Path.Combine(WebsitePath, "Views/App/Index.cshtml");

    }

}
