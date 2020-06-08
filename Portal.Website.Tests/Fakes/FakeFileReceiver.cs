using Portal.Data.Web.Form;
using System.Collections.Generic;

namespace Portal.Website.Tests.Fakes {

    public class FakeFileReceiver : IFileReceiver {

        public static readonly string EXTENSION = "png";

        public readonly IList<string> SavedFiles = new List<string>();

        public int SizeMB { get; }

        public string Name { get; }

        public FakeFileReceiver(int SizeMB, string Name) {
            this.SizeMB = SizeMB;
            this.Name = Name;
        }

        public FakeFileReceiver() : this(1, "test") {
        }

        public IEnumerable<IPostedFile> GetPostedFiles() {
            return new IPostedFile[] { new FakePostedFile(SizeMB, Name, this) };
        }

    }

}