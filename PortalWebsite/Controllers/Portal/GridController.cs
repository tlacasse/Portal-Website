using Newtonsoft.Json;
using Portal;
using Portal.Data;
using Portal.Models.Portal;
using Portal.Models.Portal.Specific;
using PortalWebsite.Data;
using PortalWebsite.Data.Logic;
using PortalWebsite.Data.Logic.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalWebsite.Controllers.Portal {

    /// <summary>
    /// Anything related to configuring the Grid of Icons.
    /// </summary>
    [RoutePrefix("api/portal/grid")]
    public class GridController : ApiController {

        /// <summary>
        /// File location for storing the JSON of the Grid Dimensions.
        /// </summary>
        public static readonly string GRID_DIMENSIONS_FILE = Path.Combine(PortalUtility.SitePath, "Portal/grid.json");

        /// <summary>
        /// Runtime state of the Grid Dimensions.
        /// </summary>
        internal static GridSize CurrentGridSize { get; set; }

        [HttpGet]
        [Route("size/get")]
        public dynamic GetDimensions() {
            return new { //anonymous type to include Max/Min
                CurrentGridSize.Width,
                CurrentGridSize.Height,
                CurrentGridSize.Max,
                CurrentGridSize.Min
            };
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<IconPosition> GetGridCells() {
            return this.LogIfError(() => {
                using (Connection connection = new Connection()) {
                    return connection.GetGridCells();
                }
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage UpdateGrid() {
            return this.LogIfError(() => {
                GridState grid = (new ObjectPost<GridState>()).GetPostedObject();
                GridState current = new GridState() { Size = CurrentGridSize };
                grid.ValidateData();
                using (Connection connection = new Connection()) {
                    current.Cells = connection.GetGridCells();
                    IEnumerable<IconPosition> toBeInactive = current.GetIconsToBeInactive(grid);
                    if (toBeInactive.Any()) {
                        connection.ExecuteNonQuery(toBeInactive.BuildInactivateQuery(), QueryOptions.Log);
                    }
                    IEnumerable<IconPosition> toBeAdded = grid.GetIconsToBeAdded(current);
                    if (toBeAdded.Any()) {
                        connection.ExecuteNonQuery(toBeAdded.BuildAddIconsQuery(), QueryOptions.Log);
                    }
                }
                CurrentGridSize = grid.Size;
                CurrentGridSize.SaveSize();
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });
        }

    }

}
