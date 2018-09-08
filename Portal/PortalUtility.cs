using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Portal {

    /// <summary>
    /// Various Utilities.
    /// </summary>
    public static class PortalUtility {

        /// <summary>
        /// The path to the Build Output, where the website is executed.
        /// </summary>
        public static string SitePath {
            get { return HttpContext.Current.Server.MapPath("~"); }
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

    }

}
