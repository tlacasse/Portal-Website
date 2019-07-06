using Portal.Data.Web;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Portal.Website {

    public class WebApiApplication : HttpApplication {

        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            IWebsiteState websiteState = RequestConfig.RegisterRequests();
            GridDataSetup.SetupGridSize(websiteState);
            GridDataSetup.SetupLastBuildTime(websiteState);
        }

    }

}
