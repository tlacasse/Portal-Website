using Portal.Data.Web;
using Portal.Messages;
using Portal.Structure.Requests;

namespace Portal.App.Portal.Requests {

    public class GridSizeRequest : IRequestOut<GridSize> {

        private IWebsiteState WebsiteState { get; set; }

        public GridSizeRequest(IWebsiteState WebsiteState) {
            this.WebsiteState = WebsiteState;
        }

        public GridSize Process() {
            return WebsiteState.ActiveIconGridSize;
        }

    }

}
