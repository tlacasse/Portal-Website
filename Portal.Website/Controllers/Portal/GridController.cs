using Portal.App.Portal.Messages;
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
        public IEnumerable<IconPosition> GetGridCells() {
            return Process(() => {
                return Get<GridCellsRequest>().Process();
            });
        }

        [HttpPost]
        [Route("update")]
        public SuccessMessage UpdateGrid(GridState grid) {
            Process(() => {
                Get<GridUpdateRequest>().Process(grid);
            });
            return new SuccessMessage() {
                Message = "Grid updated."
            };
        }

    }

}
