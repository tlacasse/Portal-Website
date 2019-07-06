using System;
using System.Linq;
using System.Reflection;

namespace Portal {

    public static class PortalUtility {

        /// <summary>
        /// The database value for the current datetime.
        /// </summary>
        public static string SqlTimestamp {
            get { return "datetime(CURRENT_TIMESTAMP, 'localtime')"; }
        }

        /// <summary>
        /// Instantiates a new object of the specified type from the default constructor.
        /// </summary>
        public static T ConstructEmpty<T>() {
            ConstructorInfo constructor = typeof(T).GetConstructors()
                .Where(c => c.GetParameters().Length == 0).Single();
            return (T)(constructor.Invoke(null));
        }

        /// <summary>
        /// Converts a name (spaces allowed) into a form safe for URLs.
        /// </summary>
        public static string UrlFormat(string name) {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;
            return string.Join("_", name.ToLower().Split(' '));
        }

        /// <summary>
        /// Converts a formatted name back into its capitalized and spaced name.
        /// </summary>
        public static string UnUrlFormat(string name) {
            return string.Join(" ", name
                .Split('_')
                .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1))
           );
        }

        /// <summary>
        /// Returns the image file extension, while ensuring the image is in an allowed set.
        /// </summary>
        public static string GetImageExtension(string contentType) {
            switch (contentType) {
                case "image/jpeg": return "jpg";
                case "image/png": return "png";
                default:
                    throw new ArgumentOutOfRangeException("Content Type");
            }
        }

        /// <summary>
        /// Returns a DateTime in a string formatted for Sqlite.
        /// </summary>
        public static string DateTimeToSqlLiteString(DateTime dateTime) {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }

}
