using Portal.App.Banking.Messages;
using Portal.App.Banking.Requests;
using Portal.Website.Structure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Portal.Website.Controllers.Banking {

    [RoutePrefix("api/banking/list")]
    public class ListController : ApiControllerBase {

        [HttpGet]
        [Route("columns/{table}")]
        public IEnumerable<ListColumn> GetListColumns(string table) {
            return Process(() => {
                return Get<ListColumnsRequest>().Process(table);
            });
        }

        [HttpGet]
        [Route("data/{table}")]
        public IEnumerable<IEnumerable<string>> GetListData(string table) {
            return Process(() => {
                return Get<ListDataRequest>().Process(table);
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage GetListData(string[] sourceData) {
            Process(() => {
                Get<ListUpdateRequest>().Process(new ListUpdate(sourceData));
            });
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }


}