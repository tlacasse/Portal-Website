using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace Portal.Website.Controllers.Portal {

    [RoutePrefix("api/portal/api")]
    public class ApiViewController : ApiController {

        [HttpGet]
        [Route("get")]
        public IEnumerable<ApiItem> GetApiItems() {
            return ReflectApi(Assembly.GetExecutingAssembly()).OrderBy(api => api.Uri);
        }

        /// <summary>
        /// Returns a list of ApiItems for an assembly.
        /// </summary>
        public static IEnumerable<ApiItem> ReflectApi(Assembly assembly) {
            foreach (Type type in assembly.GetTypes()) {
                if ((typeof(ApiController)).Equals(type.BaseType)) {
                    RoutePrefixAttribute routePrefix = (RoutePrefixAttribute)
                        type.GetCustomAttributes(typeof(RoutePrefixAttribute)).Single();
                    foreach (MethodInfo method in type.GetMethods()) {
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

}
