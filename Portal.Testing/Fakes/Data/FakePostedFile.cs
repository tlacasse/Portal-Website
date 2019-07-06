using Portal.Data.Web.Form;
using System.IO;

namespace Portal.Testing.Fakes.Data {

    public class FakePostedFile : IPostedFile {

        private static int _counter = 0;
        private static int Counter {
            get {
                return _counter++;
            }
        }

        public FakePostedFile(int ContentLength) {
            this.ContentLength = ContentLength;
            FileName = Counter.ToString();
        }

        public string FileName { get; private set; }

        public string ContentType {
            get { return "png"; }
        }

        public int ContentLength { get; private set; }

        public Stream InputStream {
            get { return null; }
        }

        public void SaveAs(string filename) {
            FakeFileReceiver.ListOfSavedFiles.Add(this);
        }

    }

}
