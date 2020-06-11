using System;

namespace Portal.Data {

    public class LoggingFailedException : Exception {

        public Exception Cause { get; }

        public LoggingFailedException(Exception e)
           : base(string.Format("[{0}] {1}", e.GetType().ToString(), e.Message)) {
            Cause = e;
        }

    }

}
