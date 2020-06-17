using Portal.App.Banking.Requests;
using Portal.App.Banking.Services;
using Portal.App.Portal.Requests;
using Portal.App.Portal.Services;
using Portal.Data;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Structure;

namespace Portal.Website {

    public class DependencyConfig {

        public static IDependencyLibrary RegisterServiceDependencies() {
            DependencyLibrary library = new DependencyLibrary();

            library.Include<IConnectionFactory>(new ConnectionFactory());
            library.Include<IWebsiteState>(new WebsiteState());
            library.Include<IFileReceiver>(new FileReceiver());
            library.Include<IIconValidatorService>(new IconValidatorService());
            library.Include<IListService>(new ListService());

            library.MarkForBuild<GridBuildRequest>(typeof(GridBuildRequest));
            library.MarkForBuild<GridCellsRequest>(typeof(GridCellsRequest));
            library.MarkForBuild<GridSizeRequest>(typeof(GridSizeRequest));
            library.MarkForBuild<GridUpdateRequest>(typeof(GridUpdateRequest));
            library.MarkForBuild<IconByNameRequest>(typeof(IconByNameRequest));
            library.MarkForBuild<IconListRequest>(typeof(IconListRequest));
            library.MarkForBuild<IconUploadRequest>(typeof(IconUploadRequest));
            library.MarkForBuild<LastGridBuildTimeRequest>(typeof(LastGridBuildTimeRequest));

            library.MarkForBuild<ListColumnsRequest>(typeof(ListColumnsRequest));
            library.MarkForBuild<ListDataRequest>(typeof(ListDataRequest));
            library.MarkForBuild<ListUpdateRequest>(typeof(ListUpdateRequest));

            library.Build();
            return library;
        }

    }

}
