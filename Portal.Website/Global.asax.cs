using Portal.Data.Sqlite;
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

            DatabaseConfig.RegisterDatabaseFactories(connectionFactory);
            RequestConfig.RegisterRequests(DatabaseConfig.Factories, connectionFactory);
        }

    }

}
