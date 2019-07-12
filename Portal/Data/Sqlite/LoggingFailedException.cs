using System;

namespace Portal.Data.Sqlite {

    public class LoggingFailedException : Exception {

        public LoggingFailedException(Exception e)
           : base(string.Format("[{0}] {1}", e.GetType().ToString(), e.Message)) {
        }

    }

}
