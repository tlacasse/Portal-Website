using Portal.App.Portal.Data;
using Portal.Data.Storage;
using Portal.Data.Web;
using Portal.Requests;

namespace Portal.App.Portal.Requests {

    public class GridSizeRequest : DependentBase, IRequestOut<GridSize> {

        public GridSizeRequest(IWebsiteState WebsiteState, IDatabaseFactory DatabaseFactory)
            : base(WebsiteState, DatabaseFactory) {
        }

        public GridSize Process() {
            return new GridSize() {
                Width = WebsiteState.GetSettingInt(Setting.PortalGridCurrentWidth),
                Height = WebsiteState.GetSettingInt(Setting.PortalGridCurrentHeight)
            };
        }

    }

}
