using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Data.Models.Portal;
using Portal.Website.Structure;
using System.Collections.Generic;
using System.Web.Http;

namespace Portal.Website.Portal.Controllers {

    [RoutePrefix("api/portal/icon")]
    public class IconController : ApiControllerBase {

        [HttpGet]
        [Route("list")]
        public IEnumerable<Icon> GetIconList() {
            return Process(() => {
                return Get<IconListRequest>().Process();
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
        public Icon UpdateIcon() {
            IconPost icon = ParseFormData<IconPost>();
            return Process(() => {
                return Get<IconUploadRequest>().Process(icon);
            });
        }

    }

}
