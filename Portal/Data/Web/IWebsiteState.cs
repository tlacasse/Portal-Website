using Portal.Messages;

namespace Portal.Data.Web {

    public interface IWebsiteState {

        string WebsitePath { get; }

        string GetPath(string relativePath);

        string IconGridSizePath { get; }

        GridSize ActiveIconGridSize { get; set; }

    }

}
