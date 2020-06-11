using Portal.Data.Web.Form;
using System.Collections.Generic;

namespace Portal.Website.Tests.Fakes {

    public class FakeEmptyFileReceiver : IFileReceiver {

        public IEnumerable<IPostedFile> GetPostedFiles() {
            return new IPostedFile[] { };
        }

    }

}
