using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Structure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Portal.Website {

    public static class DependencyConfig {

        public static DependencyContainer BuildCoreDependencies() {
            DependencyContainer container = new DependencyContainer();
            string connectionString = ConfigurationManager.ConnectionStrings["Portal"].ConnectionString;

            container.Include<IWebsiteState>(new WebsiteState());
            container.Include<IConnectionCache>(new ConnectionCache(connectionString));

            return container;
        }

    }

}