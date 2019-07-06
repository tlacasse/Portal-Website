using Portal.Models.Portal;
using Portal.Requests.Portal;
using Portal.Website.Structure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Portal.Website.Controllers.Portal {

    [RoutePrefix("api/portal/build")]
    public class BuildController : ApiControllerBase {

        [HttpGet]
        [Route("changes")]
        public IList<GridChangeItem> GetChangeHistory() {
            return Process(() => {
                return Get<GridChangeHistoryRequest>().Process(null);
            });
        }

        [HttpGet]
        [Route("lasttime")]
        public string GetLastBuildDate() {
            return Process(() => {
                return Get<LastBuildTimeRequest>().Process(null);
            });
        }

        [HttpPost]
        [Route("build")]
        public HttpResponseMessage BuildGrid() {
            return Process(() => {
                Get<BuildGridRequest>().Process(null);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });
        }

    }

}
