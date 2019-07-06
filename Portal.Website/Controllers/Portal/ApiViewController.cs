using Portal.Models.Portal;
using Portal.Requests.Portal;
using Portal.Website.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Portal.Website.Controllers.Portal {

    [RoutePrefix("api/portal/api")]
    public class ApiViewController : ApiControllerBase {

        [HttpGet]
        [Route("get")]
        public IEnumerable<ApiItem> GetIconList() {
            return Process(() => {
                IEnumerable<Type> apiControllers =
                    Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => typeof(ApiControllerBase).Equals(type.BaseType));
                return Get<ApiItemsRequest>().Process(apiControllers).OrderBy(api => api.Uri);
            });
        }

    }

}
