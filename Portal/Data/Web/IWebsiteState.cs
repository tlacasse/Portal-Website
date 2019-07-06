using Portal.Models.Portal;
using System;

namespace Portal.Data.Web {

    /// <summary>
    /// Global variables of the website.
    /// </summary>
    public interface IWebsiteState {

        /// <summary>
        /// Website Path.
        /// </summary>
        string WebsitePath { get; }

        /// <summary>
        /// Path of the current GridSize, stored in a json file.
        /// </summary>
        string CurrentGridSizePath { get; }

        /// <summary>
        /// Path of the last build DateTime, stored in a text file.
        /// </summary>
        string LastGridBuildTimePath { get; }

        /// <summary>
        /// Current dimensions of the grid.
        /// </summary>
        GridSize CurrentGridSize { get; set; }

        /// <summary>
        /// When the grid build was ran last.
        /// </summary>
        DateTime LastGridBuildTime { get; set; }

        /// <summary>
        /// Path of the Index view. This is where the actual icons are used.
        /// </summary>
        string IndexPath { get; }

        /// <summary>
        /// Path of an empty version of the Index view, to be copied from.
        /// </summary>
        string IndexEmptyPath { get; }

    }

}
