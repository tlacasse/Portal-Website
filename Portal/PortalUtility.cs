using System;
using System.Linq;
using System.Reflection;

namespace Portal {

    public static class PortalUtility {

        public static T ConstructEmpty<T>() {
            ConstructorInfo constructor = typeof(T).GetConstructors()
                .Where(c => c.GetParameters().Length == 0).Single();
            return (T)(constructor.Invoke(null));
        }

        public static string UrlFormat(string name) {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;
            return string.Join("_", name.ToLower().Split(' '));
        }

        public static string UnUrlFormat(string name) {
            return string.Join(" ", name
                .Split('_')
                .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1))
           );
        }

        public static string GetImageExtension(string contentType) {
            switch (contentType) {
                case "image/jpeg": return "jpg";
                case "image/png": return "png";
                default:
                    throw new ArgumentOutOfRangeException("Content Type");
            }
        }

    }

}
