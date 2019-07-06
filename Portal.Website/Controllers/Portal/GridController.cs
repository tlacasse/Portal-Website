using Portal.Models.Portal;
using Portal.Requests.Portal;
using Portal.Website.Structure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Portal.Requests.Portal.GridSizeRequest;

namespace Portal.Website.Controllers.Portal {

    [RoutePrefix("api/portal/grid")]
    public class GridController : ApiControllerBase {

        [HttpGet]
        [Route("size")]
        public ExtendedGridSize GetDimensions() {
            return Process(() => {
                return Get<GridSizeRequest>().Process(null);
            });
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<IconPosition> GetGridCells() {
            return Process(() => {
                return Get<GridCellsRequest>().Process(null);
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage UpdateGrid(GridState newGrid) {
            return Process(() => {
                Get<GridUpdateRequest>().Process(newGrid);
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });
        }

    }

}
