using System.Collections.Generic;

namespace Portal.Data.Web.Form {

    public interface IFileReceiver {

        IEnumerable<IPostedFile> GetPostedFiles();

    }

}
