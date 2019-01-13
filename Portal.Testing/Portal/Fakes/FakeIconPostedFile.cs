using PortalWebsite.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTesting.Portal.Fakes {

    public class FakeIconPostedFile : IPostedFile {

        public static readonly List<string> SavedFiles = new List<string>();

        public string FileName => throw new NotImplementedException();

        public Stream InputStream => throw new NotImplementedException();

        public string ContentType {
            get { return "image/png"; }
        }

        public int ContentLength { get; }

        public FakeIconPostedFile(int ContentLength) {
            this.ContentLength = ContentLength;
        }

        public void SaveAs(string filename) {
            SavedFiles.Add(filename);
        }

    }

}
