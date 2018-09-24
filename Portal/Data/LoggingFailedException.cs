using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data {

    public class LoggingFailedException : Exception {

        public LoggingFailedException(Exception e)
            : base(string.Format("[{0}] {1}", e.GetType().ToString(), e.Message)) {
        }

    }

}
