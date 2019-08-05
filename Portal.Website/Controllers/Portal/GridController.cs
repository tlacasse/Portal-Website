using Portal.App.Portal.Data;
using Portal.App.Portal.Requests;
using Portal.Website.Structure;
using System.Web.Http;

namespace Portal.Website.Controllers.Portal {

    [RoutePrefix("api/portal/grid")]
    public class GridController : ApiControllerBase {

        [HttpGet]
        [Route("size")]
        public GridSize GetDimensions() {
            return Process(() => {
                return Get<GridSizeRequest>().Process();
            });
        }

        /*[HttpGet]
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
        }*/

    }

}
