using Portal.App.Portal.Requests;
using Portal.Data.Sqlite;
using Portal.Data.Storage;
using Portal.Data.Web;
using Portal.Data.Web.Form;
using Portal.Requests;
using Portal.Requests.Processors;
using Portal.Structure;
using System.Collections.Generic;

namespace Portal.Website {

    public static class RequestConfig {

        public static IRequestProcessor RequestProcessor { get; private set; }

        public static IReadOnlyRegisterLibrary<IRequest> RequestLibrary {
            get { return requestLibrary; }
        }

        private static readonly RegisterLibrary<IRequest> requestLibrary =
            new RegisterLibrary<IRequest>();

        public static void RegisterRequests(IReadOnlyDictionary<string, IDatabaseFactory> databaseFactories,
                IConnectionFactory connectionFactory, IWebsiteState websiteState) {

            IFileReceiver fileReceiver = new FileReceiver();

            requestLibrary.Include(new GridSizeRequest(websiteState, databaseFactories["Portal"]));
            requestLibrary.Include(new IconByNameRequest(websiteState, databaseFactories["Portal"]));
            requestLibrary.Include(new IconListRequest(websiteState, databaseFactories["Portal"]));
            requestLibrary.Include(new IconUploadRequest(websiteState, databaseFactories["Portal"], fileReceiver));

            RequestProcessor = new LoggingRequestProcessor(connectionFactory);
        }

    }

}
