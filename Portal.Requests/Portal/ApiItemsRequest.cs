using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Portal.Requests.Portal {

    public class ApiItemsRequest : DependentBase, IRequest<IEnumerable<Type>, IEnumerable<ApiItem>> {

        public ApiItemsRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public IEnumerable<ApiItem> Process(IEnumerable<Type> model) {
            foreach (Type apiController in model) {
                RoutePrefixAttribute routePrefix = (RoutePrefixAttribute)
                        apiController.GetCustomAttributes(typeof(RoutePrefixAttribute)).Single();
                foreach (MethodInfo method in apiController.GetMethods()) {
                    RouteAttribute route = (RouteAttribute)
                        method.GetCustomAttributes(typeof(RouteAttribute))
                        .FirstOrDefault();
                    Attribute verb = method.GetCustomAttributes()
                        .Where(a => a.GetType().ToString().StartsWith("System.Web.Http.Http"))
                        .FirstOrDefault();
                    if (route != null && verb != null) {
                        yield return new ApiItem() {
                            Uri = routePrefix.Prefix + '/' + route.Template,
                            Verb = verb.GetType().ToString().Replace("Attribute", "").Replace("System.Web.Http.Http", "")
                        };
                    }
                }
            }
        }

    }

}
