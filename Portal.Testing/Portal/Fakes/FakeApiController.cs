using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Portal.Testing.Portal.Fakes {

    [RoutePrefix("testing/prefix")]
    public class FakeApiController : ApiController {

        [HttpGet]
        [Route("gettest")]
        public string GetTest() {
            return "";
        }

        [HttpPost]
        [Route("posttest")]
        public HttpResponseMessage PostTest() {
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

    }

}
