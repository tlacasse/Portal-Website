using Portal.Data.Web.Form;
using System;
using System.IO;

namespace Portal.Website.Tests.Fakes {

    public class FakePostedFile : IPostedFile {

        public string FileName { get; }

        public string ContentType => "image/png";

        public int ContentLength { get; }

        public Stream InputStream => throw new NotImplementedException();

        public FakeFileReceiver FileReceiver { get; }

        public FakePostedFile(int lenMB, string name, FakeFileReceiver FileReceiver) {
            this.ContentLength = lenMB * 1024 * 1024;
            this.FileName = name;
            this.FileReceiver = FileReceiver;
        }

        public void SaveAs(string filename) {
            FileReceiver.SavedFiles.Add(filename);
        }

    }

}
