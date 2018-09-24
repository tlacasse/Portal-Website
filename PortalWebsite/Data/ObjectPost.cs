using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalWebsite.Data {

    /// <summary>
    /// A posted JS object, which is likely to be too complex / not user defined to be in FormData.
    /// </summary>
    internal class ObjectPost<Obj> : IObjectPost<Obj> {

        /// <summary>
        /// The converted object from the Request.
        /// </summary>
        private Obj Result { get; }

        /// <summary>
        /// Reads a http request for an object.
        /// </summary>
        public ObjectPost() {
            HttpRequest request = HttpContext.Current.Request;
            string result;
            using (Stream stream = request.InputStream) {
                using (StreamReader streamReader = new StreamReader(stream, request.ContentEncoding)) {
                    result = streamReader.ReadToEnd();
                }
            }
            Result = JsonConvert.DeserializeObject<Obj>(result);
        }

        /// <summary>
        /// Gets the converted object from a post.
        /// </summary>
        /// <returns></returns>
        public Obj GetPostedObject() {
            return Result;
        }

    }

}
