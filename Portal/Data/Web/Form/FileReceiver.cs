using System.Web;

namespace Portal.Data.Web.Form {

    public class FileReceiver : IFileReceiver {

        public IPostedFile GetPostedFile() {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            if (files.Count == 0) return null;
            return new HttpPostedFileWrapper(files.Get(0));
        }
    }

}
