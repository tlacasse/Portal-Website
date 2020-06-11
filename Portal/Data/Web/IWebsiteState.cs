using Portal.Messages;
using System;

namespace Portal.Data.Web {

    public interface IWebsiteState {

        string WebsitePath { get; }

        string GetPath(string relativePath);

        string IconGridSizePath { get; }

        GridSize ActiveIconGridSize { get; set; }

        string LastGridBuildTimePath { get; }

        DateTime LastGridBuildTime { get; set; }

        string IndexEmptyPath { get; }

        string IndexBuiltPath { get; }

    }

}
