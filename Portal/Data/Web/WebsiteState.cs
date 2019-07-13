using System.IO;
using System.Web;

namespace Portal.Data.Web {

    public class WebsiteState : IWebsiteState {

        public string WebsitePath => HttpContext.Current.Server.MapPath("~");

        public string GetPath(string relativePath) {
            return Path.Combine(WebsitePath, relativePath);
        }
    }

}
