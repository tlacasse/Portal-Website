using Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;

namespace Portal.Website.Data {

    /// <summary>
    /// Wrapper of a Request, with form data and files.
    /// </summary>
    internal class FormPost : IFormPost {

        /// <summary>
        /// Empty location to put some temp files.
        /// </summary>
        private static readonly string FORM_DUMP = Path.Combine(PortalUtility.SitePath, "Scripts");

        /// <summary>
        /// Form Inputs.
        /// </summary>
        private NameValueCollection FormData { get; }

        /// <summary>
        /// Uploaded Files.
        /// </summary>
        private HttpFileCollection Files { get; }

        /// <summary>
        /// Creates a new FormPost wrapper around a request.
        /// </summary>
        public static async Task<FormPost> LoadDataAsync(HttpRequestMessage request) {
            MultipartFormDataStreamProvider provider = new MultipartFormDataStreamProvider(FORM_DUMP);
            await request.Content.ReadAsMultipartAsync(provider);
            return new FormPost(provider.FormData, HttpContext.Current.Request.Files);
        }

        private FormPost(NameValueCollection FormData, HttpFileCollection Files) {
            this.FormData = FormData;
            this.Files = Files;
        }

        /// <summary>
        /// Returns the value of the provided key.
        /// </summary>
        public string this[string key] {
            get {
                return FormData[key]?.Trim();
            }
        }

        /// <summary>
        /// Returns the uploaded file, validating that the file is not to big. Returns null if no files were uploaded.
        /// </summary>
        public IPostedFile GetPostedFile() {
            if (Files.Count == 0) return null;
            HttpPostedFile file = Files.Get(0);
            return new HttpPostedFileWrapper(file);
        }

        private class HttpPostedFileWrapper : IPostedFile {

            private HttpPostedFile HttpPostedFile { get; }
            public string FileName => HttpPostedFile.FileName;
            public string ContentType => HttpPostedFile.ContentType;
            public int ContentLength => HttpPostedFile.ContentLength;
            public Stream InputStream => HttpPostedFile.InputStream;

            public void SaveAs(string filename) {
                HttpPostedFile.SaveAs(filename);
            }

            public HttpPostedFileWrapper(HttpPostedFile HttpPostedFile) {
                this.HttpPostedFile = HttpPostedFile;
            }

        }

    }

}
