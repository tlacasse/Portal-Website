using Portal.Data.Sqlite;
using Portal.Data.Web;
using static Portal.Requests.Portal.GridSizeRequest;

namespace Portal.Requests.Portal {

    public class GridSizeRequest : DependentBase, IRequest<Void, ExtendedGridSize> {

        public GridSizeRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public ExtendedGridSize Process(Void model) {
            return new ExtendedGridSize() {
                Width = WebsiteState.CurrentGridSize.Width,
                Height = WebsiteState.CurrentGridSize.Height,
                Min = WebsiteState.CurrentGridSize.Min,
                Max = WebsiteState.CurrentGridSize.Max,
            };
        }

        public class ExtendedGridSize {
            internal int Width { get; set; }
            internal int Height { get; set; }
            internal int Min { get; set; }
            internal int Max { get; set; }
        }
    }

}
