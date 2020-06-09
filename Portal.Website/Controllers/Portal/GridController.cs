using Portal.App.Portal.Requests;
using Portal.Data.Models.Portal;
using Portal.Messages;
using Portal.Website.Structure;
using System.Collections.Generic;
using System.Web.Http;

namespace Portal.Website.Controllers.Portal {

    [RoutePrefix("api/portal/grid")]
    public class GridController : ApiControllerBase {

        [HttpGet]
        [Route("size")]
        public GridSize GetGridSize() {
            return Process(() => {
                return Get<GridSizeRequest>().Process();
            });
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<IconPosition> GetGridItems() {
            return Process(() => {
                return Get<GridItemsRequest>().Process();
            });
        }

    }

}
