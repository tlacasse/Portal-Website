using Portal.Structure;
using System.Collections.Generic;
using System.Web;

namespace Portal.Data.Web.Form {

    public class FileReceiver : IFileReceiver, IService<IFileReceiver> {

        public IEnumerable<IPostedFile> GetPostedFiles() {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            if (files.Count == 0) return new IPostedFile[] { };

            List<HttpPostedFileWrapper> results = new List<HttpPostedFileWrapper>();
            for (int i = 0; i < files.Count; i++) {
                results.Add(new HttpPostedFileWrapper(files.Get(i)));
            }

            return results;
        }

    }

}
