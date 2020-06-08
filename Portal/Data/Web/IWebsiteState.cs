
namespace Portal.Data.Web {

    public interface IWebsiteState {

        string WebsitePath { get; }

        string GetPath(string relativePath);

    }

}
