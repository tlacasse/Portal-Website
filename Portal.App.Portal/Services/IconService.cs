using Portal.App.Portal.Messages;
using Portal.Data.Models.Portal;
using Portal.Data.Web.Form;
using System.Text.RegularExpressions;

namespace Portal.App.Portal.Services {

    public class IconService : IIconService {

        public static readonly int MAX_ICON_MB = 10;

        public void ValidateIconPost(IconPost iconPost) {
            if (string.IsNullOrWhiteSpace(iconPost.Name))
                throw new PortalException(IconResult.FAIL_NO_NAME, "Must have a value.");
            if (string.IsNullOrWhiteSpace(iconPost.Link))
                throw new PortalException(IconResult.FAIL_NO_LINK, "Must have a value.");
            if (iconPost.Name.Length > 30)
                throw new PortalException(IconResult.FAIL_LONG_NAME, "Length must be less than 30 characters.");
            if (iconPost.Link.Length > 500)
                throw new PortalException(IconResult.FAIL_LONG_LINK, "Length must be less than 500 characters.");

            string nameWithOnlyValidChars = Regex.Replace(iconPost.Name, @"[^a-zA-Z0-9 ]", "");
            if (iconPost.Name != nameWithOnlyValidChars)
                throw new PortalException(IconResult.FAIL_INVALID_NAME, "Must only have letters, numbers, and spaces.");
        }

        public void ValidateIconPostFile(Icon icon, IPostedFile file) {
            if (icon.IsNew && string.IsNullOrWhiteSpace(icon.Image)) {
                throw new PortalException(IconResult.FAIL_NO_IMAGE, "Must have a value.");
            }
            if (icon.IsNew && file == null) {
                throw new PortalException(IconResult.FAIL_NO_FILE, "Must be uploaded.");
            }
            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new PortalException(IconResult.FAIL_FILE_TOO_LARGE, string.Format("Is too large (limit {0}MB)", MAX_ICON_MB));
            }
        }

    }

}
