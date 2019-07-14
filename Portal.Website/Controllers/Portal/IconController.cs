using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Website.Structure;
using System.Collections.Generic;
using System.Web.Http;

namespace Portal.Website.Portal.Controllers {

    [RoutePrefix("api/portal/icon")]
    public class IconController : ApiControllerBase {

        [HttpGet]
        [Route("list")]
        public IReadOnlyList<Icon> GetIconList() {
            return Process(() => {
                return Get<IconListRequest>().Process(null);
            });
        }

    }

}
