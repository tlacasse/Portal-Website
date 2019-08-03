using Portal.Data.Sqlite;
using Portal.Data.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Portal.Website {

    public class WebApiApplication : HttpApplication {

        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            string connectionString = ConfigurationManager.ConnectionStrings["Portal"].ConnectionString;
            IConnectionFactory connectionFactory = new ConnectionFactory(connectionString);
            IWebsiteState websiteState = new WebsiteState(GetDefaultSettings());

            DatabaseConfig.RegisterDatabaseFactories(connectionFactory);
            RequestConfig.RegisterRequests(DatabaseConfig.Factories, connectionFactory, websiteState);
        }

        private Dictionary<Setting, string> GetDefaultSettings() {
            return new Dictionary<Setting, string>() {
                [Setting.PortalGridCurrentWidth] = ConfigurationManager.AppSettings["PortalGridDefaultWidth"],
                [Setting.PortalGridCurrentHeight] = ConfigurationManager.AppSettings["PortalGridDefaultHeight"],
                [Setting.PortalGridMaxSize] = ConfigurationManager.AppSettings["PortalGridMaxSize"],
                [Setting.PortalGridMinSize] = ConfigurationManager.AppSettings["PortalGridMinSize"]
            };
        }

    }

}
