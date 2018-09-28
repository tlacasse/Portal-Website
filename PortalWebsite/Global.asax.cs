using Newtonsoft.Json;
using Portal.Models.Portal;
using PortalWebsite.Controllers.Portal;
using PortalWebsite.Data.Logic.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PortalWebsite {

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
        }

    }

}
