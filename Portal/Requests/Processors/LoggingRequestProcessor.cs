﻿using Portal.Data.Sqlite;
using System;

namespace Portal.Requests.Processors {

    public class LoggingRequestProcessor : IRequestProcessor {

        private IConnectionFactory ConnectionFactory { get; }

        public LoggingRequestProcessor(IConnectionFactory ConnectionFactory) {
            this.ConnectionFactory = ConnectionFactory;
        }

        public T Process<T>(Func<T> requestCall) {
            //try {
            return requestCall.Invoke();
            //} catch (Exception e) {
            //    LogError(e);
            //    throw;
            //}
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
