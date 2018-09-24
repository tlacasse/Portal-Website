using Portal.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PortalWebsite.Data.Logic {

    public static class ApiControllerExtensions {

        /// <summary>
        /// Executes code, and will log any exception encountered.
        /// </summary>
        public static T LogIfError<T>(this ApiController _, Func<T> function) {
            try {
                return function.Invoke();
            } catch (LoggingFailedException lfe) {
                throw lfe;
            } catch (Exception e) {
                using (Connection connection = new Connection()) {
                    connection.Log(e);
                }
                throw e;
            }
        }

    }

}
