using Newtonsoft.Json;
using Portal;
using Portal.Models.Portal;
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

    }

}
