using Newtonsoft.Json;
using Portal.Data.Web;
using Portal.Models.Portal;
using System;
using System.IO;

namespace Portal.Website {

    public static class GridDataSetup {

        public static void SetupGridSize(IWebsiteState websiteState) {
            if (File.Exists(websiteState.CurrentGridSizePath)) {
                string json = File.ReadAllText(websiteState.CurrentGridSizePath);
                websiteState.CurrentGridSize = JsonConvert.DeserializeObject<GridSize>(json);
            } else {
                // set automatically saves to file
                websiteState.CurrentGridSize = new GridSize() {
                    Width = 15,
                    Height = 6
                };
            }
        }

        public static void SetupLastBuildTime(IWebsiteState websiteState) {
            if (File.Exists(websiteState.LastGridBuildTimePath)) {
                string datetimeStr = File.ReadAllText(websiteState.LastGridBuildTimePath);
                websiteState.LastGridBuildTime = DateTime.Parse(datetimeStr);
            } else {
                // set automatically saves to file
                websiteState.LastGridBuildTime = DateTime.MinValue;
            }
        }

    }

}
