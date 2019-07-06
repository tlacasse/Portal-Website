using Portal.Models.Portal;
using Portal.Website.Structure;
using Portal.Requests.Portal;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace Portal.Website.Portal.Controllers {

    [RoutePrefix("api/portal/icon")]
    public class IconController : ApiControllerBase {

        [HttpGet]
        [Route("list")]
        public IList<Icon> GetIconList() {
            return Process(() => {
                return Get<IconListRequest>().Process(null);
            });
        }

        [HttpGet]
        [Route("get/{name}")]
        public Icon GetIconByName(string name) {
            return Process(() => {
                return Get<IconByNameRequest>().Process(name);
            });
        }

        [HttpPost]
        [Route("post")]
        public HttpResponseMessage UpdateIconAsync(Icon icon) {
            return Process(() => {
                Get<IconUploadRequest>().Process(icon);
                return Request.CreateResponse(HttpStatusCode.Created);
            });
        }

    }

}
