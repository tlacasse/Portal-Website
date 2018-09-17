using Newtonsoft.Json;
using Portal;
using Portal.Models.Portal;
using PortalWebsite.Controllers.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalWebsite.Data.Logic.Portal {

    public static class GridSizeExtensions {

        /// <summary>
        /// Saves the JSON of a GridSize to the specified path.
        /// </summary>
        public static void SaveSizeToPath(this GridSize size, string path) {
            string json = JsonConvert.SerializeObject(size);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Saves the JSON of a GridSize to the location for the Website.
        /// </summary>
        public static void SaveSize(this GridSize size) {
            size.SaveSizeToPath(GridController.GRID_DIMENSIONS_FILE);
        }

    }

}
