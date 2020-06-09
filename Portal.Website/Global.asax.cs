using Newtonsoft.Json;
using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Web;
using Portal.Messages;
using Portal.Structure;
using Portal.Structure.Requests;
using System;
using System.IO;
using System.Web.Http;
using System.Web.Routing;

namespace Portal.Website {

    public class WebApiApplication : System.Web.HttpApplication {

        internal static IDependencyLibrary Services;
        internal static IRequestProcessor RequestProcessor;

        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Services = DependencyConfig.RegisterServiceDependencies();
            RequestProcessor = RequestProcessorConfig.BuildNestedRequestProcessor(Services);

            //CreatePortalIcon();
            EnsureGridSizeFileExists();
        }

        private void CreatePortalIcon() {
            using (var setup = new Connection()) {
                setup.Icons.Add(new Icon() {
                    Name = "Portal",
                    Image = "png",
                    Link = "http://localhost:5527/app/portal#!/",
                    DateCreated = DateTime.Now,
                    DateChanged = DateTime.Now
                });
                setup.SaveChanges();
            }
        }

        private void EnsureGridSizeFileExists() {
            IWebsiteState ws = Services.Get<IWebsiteState>();
            if (File.Exists(ws.IconGridSizePath)) {
                string json = File.ReadAllText(ws.IconGridSizePath);
                ws.ActiveIconGridSize = JsonConvert.DeserializeObject<GridSize>(json);
            } else {
                // set automatically saves to file
                ws.ActiveIconGridSize = GridSize.BuildDefault();
            }
        }

    }

}
