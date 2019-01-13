using Newtonsoft.Json;
using Portal;
using Portal.Models.Portal;
using Portal.Website.Controllers.Portal;
using Portal.Website.Data.Logic.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Portal.Website {

    public class WebApiApplication : HttpApplication {

        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if (File.Exists(GridController.GRID_DIMENSIONS_FILE)) {
                GridController.CurrentGridSize =
                    JsonConvert.DeserializeObject<GridSize>(File.ReadAllText(GridController.GRID_DIMENSIONS_FILE));
            } else {
                GridController.CurrentGridSize = new GridSize() {
                    Width = 15,
                    Height = 6
                };
                GridController.CurrentGridSize.SaveSize();
            }

            if (File.Exists(BuildController.LAST_BUILD_FILE)) {
                BuildController.LastBuildTime = DateTime.Parse(File.ReadAllText(BuildController.LAST_BUILD_FILE));
            } else {
                BuildController.LastBuildTime = DateTime.MinValue;
                File.WriteAllText(BuildController.LAST_BUILD_FILE, BuildController.LastBuildTime.ToString());
            }

            File.Copy(BuildController.FRONT_PAGE_PATH, BuildController.FRONT_PAGE_PATH_SAVE, true);
        }

    }

}
