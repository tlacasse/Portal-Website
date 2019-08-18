using Portal.App.Portal.Messages;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Data.Web.Form;
using Portal.Structure;
using System.Text.RegularExpressions;

namespace Portal.App.Portal.Services {

    public class IconService : IIconService, IService<IIconService> {

        public static readonly int MAX_ICON_MB = 10;

        public IconService() {
        }

        public void ValidateIconPost(Icon icon, IPostedFile file) {
            if (string.IsNullOrWhiteSpace(icon.Name))
                throw new PortalException("Icon Name", "Must have a value.");
            if (string.IsNullOrWhiteSpace(icon.Link))
                throw new PortalException("Icon Link", "Must have a value.");
            if (icon.IsNew && string.IsNullOrWhiteSpace(icon.Image))
                throw new PortalException("Icon Image", "Must have a value.");

            if (icon.Name.Length > 30)
                throw new PortalException("Icon Name", "Length must be less than 30 characters.");
            if (icon.Link.Length > 500)
                throw new PortalException("Icon Link", "Length must be less than 500 characters.");

            string nameWithOnlyValidChars = Regex.Replace(icon.Name, @"[^a-zA-Z0-9 ]", "");
            if (icon.Name != nameWithOnlyValidChars)
                throw new PortalException("Icon Name", "Must only have letters, numbers, and spaces.");

            if (icon.IsNew && file == null) {
                throw new PortalException("Icon Image File", "Must be uploaded.");
            }
            if (file != null && file.ContentLength > MAX_ICON_MB * 1024 * 1024) {
                throw new PortalException("Icon Image File", string.Format("Is too large (limit {0}MB)", MAX_ICON_MB));
            }
        }

    }

}
