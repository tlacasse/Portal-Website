using Portal.App.Portal.Messages;
using Portal.Data.Models.Portal;
using Portal.Data.Web.Form;
using Portal.Messages;
using System.Text.RegularExpressions;

namespace Portal.App.Portal.Services {

    public class IconValidatorService : IIconValidatorService {

        public static readonly int MAX_ICON_MB = 10;

        public void ValidateIconPost(IconPost iconPost) {
            if (string.IsNullOrWhiteSpace(iconPost.Name))
                throw new PortalException(IconResult.FAIL_NO_NAME, "Name must have a value.");
            if (string.IsNullOrWhiteSpace(iconPost.Link))
                throw new PortalException(IconResult.FAIL_NO_LINK, "Link must have a value.");
            if (iconPost.Name.Length > 30)
                throw new PortalException(IconResult.FAIL_LONG_NAME, "Name length must be less than 30 characters.");
            if (iconPost.Link.Length > 500)
                throw new PortalException(IconResult.FAIL_LONG_LINK, "Link length must be less than 500 characters.");

            string nameWithOnlyValidChars = Regex.Replace(iconPost.Name, @"[^a-zA-Z0-9 ]", "");
            if (iconPost.Name != nameWithOnlyValidChars)
                throw new PortalException(IconResult.FAIL_INVALID_NAME, "Name must only have letters, numbers, and spaces.");
        }

        public void ValidateIconPostFile(Icon icon, IPostedFile file) {
            if (icon.IsNew && string.IsNullOrWhiteSpace(icon.Image))
                throw new PortalException(IconResult.FAIL_NO_IMAGE, "Image extension must have a value.");
            if (icon.IsNew && file == null)
                throw new PortalException(IconResult.FAIL_NO_FILE, "Image file must be uploaded.");
            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024)
                throw new PortalException(IconResult.FAIL_FILE_TOO_LARGE, string.Format("Image file is too large (limit {0}MB)", MAX_ICON_MB));
        }

        public void ValidateIconGridState(GridState grid) {
            ValidateIconGridSize(grid.Size);
            ValidateIconGridSize(grid.Size);
            grid.Cells.ForEach(i => ValidateIconPosition(i, grid.Size));
        }

        public void ValidateIconGridSize(GridSize size) {
            if (size.Width > size.Max)
                throw new PortalException(GridResult.FAIL_WIDTH_TOO_LARGE, string.Format("Maximum width is {0}.", size.Max));
            if (size.Height < size.Min)
                throw new PortalException(GridResult.FAIL_HEIGHT_TOO_SMALL, string.Format("Minimum width is {0}.", size.Min));
            if (size.Width < size.Height)
                throw new PortalException(GridResult.FAIL_HEIGHT_LARGER_THAN_WIDTH, "Width must be greater than Height.");
        }

        public void ValidateIconPosition(IconPosition icon, GridSize size) {
            if (icon.XCoord < 0)
                throw new PortalException(GridResult.FAIL_ICON_BOUNDS, "X coord must be at least 0.");
            if (icon.YCoord < 0)
                throw new PortalException(GridResult.FAIL_ICON_BOUNDS, "Y coord must be at least 0.");
            if (icon.XCoord > size.Width - 1)
                throw new PortalException(GridResult.FAIL_ICON_BOUNDS, string.Format("Maximum X coord is {0}.", size.Width));
            if (icon.YCoord > size.Height - 1)
                throw new PortalException(GridResult.FAIL_ICON_BOUNDS, string.Format("Maximum Y coord is {0}.", size.Height));
        }

    }

}
