using Portal.App.Portal.Messages;
using Portal.Data.Models.Portal;
using Portal.Data.Web.Form;

namespace Portal.App.Portal.Services {

    public interface IIconService {

        void ValidateIconPost(IconPost iconPost);

        void ValidateIconPostFile(Icon icon, IPostedFile file);

    }

}
