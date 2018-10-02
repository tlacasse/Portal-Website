using Portal;
using Portal.Data;
using Portal.Models.Portal;
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

    [RoutePrefix("api/portal/build")]
    public class BuildController : ApiController {

        /// <summary>
        /// HTML comment in Grid to inject Icons.
        /// </summary>
        public static readonly string GRID_INJECT_LOCATION = "<!-- Icons -->";

        /// <summary>
        /// Path of file of the Grid front page.
        /// </summary>
        public static readonly string LAST_BUILD_FILE = Path.Combine(PortalUtility.SitePath, "Portal/lastbuildtime.txt");

        /// <summary>
        /// Path of file of the Grid front page.
        /// </summary>
        public static readonly string FRONT_PAGE_PATH = Path.Combine(PortalUtility.SitePath, "Views/App/Index.cshtml");

        /// <summary>
        /// Path of file of where the Grid front page will be copied from.
        /// </summary>
        public static readonly string FRONT_PAGE_PATH_SAVE = Path.Combine(PortalUtility.SitePath, "Portal/_Index.cshtml");

        /// <summary>
        /// Runtime storage of the last time the Grid was built.
        /// </summary>
        public static DateTime LastBuildTime { get; set; }

        [HttpGet]
        [Route("changes")]
        public IEnumerable<GridChangeItem> GetChangeHistory() {
            return this.LogIfError(() => {
                using (Connection connection = new Connection()) {
                    return connection.GetGridChanges();
                }
            });
        }

        [HttpGet]
        [Route("lasttime")]
        public string GetLastBuildDate() {
            return LastBuildTime.ToString("F");
        }

        [HttpPost]
        [Route("build")]
        public HttpResponseMessage BuildGrid() {
            return this.LogIfError(() => {
                GridState grid;
                using (Connection connection = new Connection()) {
                    grid = connection.GetCurrentGridState();
                    connection.Log("Grid Build", grid.ToString());
                }
                string gridHtml = grid.BuildGridHTML();
                File.WriteAllText(FRONT_PAGE_PATH,
                    File.ReadAllText(FRONT_PAGE_PATH_SAVE).Replace(GRID_INJECT_LOCATION, gridHtml)
                );
                LastBuildTime = DateTime.Now;
                File.WriteAllText(LAST_BUILD_FILE, LastBuildTime.ToString());
                return Request.CreateResponse(HttpStatusCode.Accepted);
            });
        }

    }

}
