using System;
using Portal.Data.Sqlite;
using Portal.Data.Web;

namespace Portal.Requests.Processors {

    public class LoggingRequestProcessor : DependentBase, IRequestProcessor {

        public LoggingRequestProcessor(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
                : base(ConnectionFactory, WebsiteState) {
        }

        public T Process<T>(Func<T> requestCall) {
            try {
                return requestCall.Invoke();
            } catch (Exception e) {
                LogError(e);
                throw e;
            }
        }

        private void LogError(Exception e) {
            LoggingFailedException lfe = e as LoggingFailedException;
            if (lfe == null) {
                using (IConnection connection = ConnectionFactory.Create()) {
                    connection.Log(e);
                }
            }
        }

    }

}
