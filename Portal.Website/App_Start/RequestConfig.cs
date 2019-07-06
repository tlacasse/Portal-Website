using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Requests;
using Portal.Requests.Portal;
using System.Configuration;

namespace Portal.Website {

    public static class RequestConfig {

        public static RequestLibrary RequestLibrary { get; private set; }
        public static IRequestProcessor RequestProcessor { get; private set; }

        public static IWebsiteState RegisterRequests() {
            string connectionString = ConfigurationManager.ConnectionStrings["Portal"].ConnectionString;

            IConnectionFactory connectionFactory = new ConnectionFactory(connectionString);
            IWebsiteState websiteState = new WebsiteState();
            IFileReceiver fileReceiver = new FileReceiver();

            RequestLibrary = new RequestLibrary();

            RequestLibrary.Include(new ApiItemsRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new BuildGridRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new GridCellsRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new GridChangeHistoryRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new GridSizeRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new GridUpdateRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new IconByNameRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new IconListRequest(connectionFactory, websiteState));
            RequestLibrary.Include(new IconUploadRequest(connectionFactory, websiteState, fileReceiver));
            RequestLibrary.Include(new LastBuildTimeRequest(connectionFactory, websiteState));

            return websiteState;
        }

    }

}
