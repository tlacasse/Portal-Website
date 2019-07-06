using System.IO;
using System.Web;

namespace Portal.Data.Web.Form {

    public class HttpPostedFileWrapper : IPostedFile {

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
