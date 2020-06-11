using Portal.App.Portal.Messages;
using Portal.Data.Models.Portal;
using Portal.Data.Web.Form;
using Portal.Messages;

namespace Portal.App.Portal.Services {

    public interface IIconValidatorService {

        void ValidateIconPost(IconPost iconPost);

        void ValidateIconPostFile(Icon icon, IPostedFile file);

        void ValidateIconGridState(GridState grid);

        void ValidateIconGridSize(GridSize size);

        void ValidateIconPosition(IconPosition icon, GridSize size);

    }

}
