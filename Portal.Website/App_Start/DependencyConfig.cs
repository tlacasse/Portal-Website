using Portal.App.Portal.Requests;
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

            library.MarkForBuild<IconListRequest>(typeof(IconListRequest));
            library.MarkForBuild<IconByNameRequest>(typeof(IconByNameRequest));

            library.Build();
            return library;
        }

    }

}
