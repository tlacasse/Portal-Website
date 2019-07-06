using Portal.Data.Web.Form;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Testing.Fakes.Data {

    public class FakeFileReceiver : IFileReceiver {

        public static List<IPostedFile> ListOfSavedFiles = new List<IPostedFile>();

        public static bool ContainsSavedFile(IPostedFile check) {
            return ListOfSavedFiles.Where(f => f.FileName == check.FileName).SingleOrDefault() != null;
        }

        public FakeFileReceiver(int ContentLength) {
            this.PostedFile = new FakePostedFile(ContentLength);
        }

        private IPostedFile PostedFile { get; }

        public IPostedFile GetPostedFile() {
            return PostedFile;
        }

    }

}
