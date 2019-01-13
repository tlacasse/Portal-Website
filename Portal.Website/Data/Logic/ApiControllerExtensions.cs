using Portal.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Portal.Website.Data.Logic {

    public static class ApiControllerExtensions {

        /// <summary>
        /// Executes code, and will log any exception encountered.
        /// </summary>
        public static T LogIfError<T>(this ApiController _, Func<T> function) {
            try {
                return function.Invoke();
            } catch (Exception e) {
                LogError(e);
                throw e;
            }
        }

        /// <summary>
        /// Executes asynchronous code, and will log any exception encountered.
        /// </summary>
        public static async Task<T> LogIfErrorAsync<T>(this ApiController _, Func<Task<T>> function) {
            try {
                return await function.Invoke();
            } catch (Exception e) {
                LogError(e);
                throw e;
            }
        }

        private static void LogError(Exception e) {
            LoggingFailedException lfe = e as LoggingFailedException;
            if (lfe == null) {
                using (Connection connection = new Connection()) {
                    connection.Log(e);
                }
            }
        }

    }

}
