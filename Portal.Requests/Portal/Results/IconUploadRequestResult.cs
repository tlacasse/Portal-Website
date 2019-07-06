using Portal.Data.Web.Form;
using Portal.Models.Portal;

namespace Portal.Requests.Portal.Results {

    public class IconUploadRequestResult {

        public IPostedFile PostedFile { get; set; }

        public Icon SubmittedIcon { get; set; }

        public Icon SavedIcon { get; set; }

    }

}
