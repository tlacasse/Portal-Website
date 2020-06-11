using Portal.Data;
using System;

namespace Portal.Structure.Requests.Processors {

    public class ErrorLoggingRequestProcessor : IRequestProcessor {

        private IRequestProcessor InnerProcessor { get; }
        private IConnectionFactory ConnectionFactory { get; }

        public ErrorLoggingRequestProcessor(IRequestProcessor InnerProcessor, IConnectionFactory ConnectionFactory) {
            this.InnerProcessor = InnerProcessor;
            this.ConnectionFactory = ConnectionFactory;
        }

        public T Process<T>(Func<T> requestCall) {
            try {
                return InnerProcessor.Process(requestCall);
            } catch (Exception e) {
                LogError(e);
                throw;
            }
        }

        public void Process(Action requestCall) {
            try {
                InnerProcessor.Process(requestCall);
            } catch (Exception e) {
                LogError(e);
                throw;
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
