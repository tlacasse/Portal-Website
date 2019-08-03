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

    }

}
