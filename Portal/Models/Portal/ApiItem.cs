using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents the Api Controller methods.
    /// </summary>
    public class ApiItem {

        /// <summary>
        /// HTTP Verb.
        /// </summary>
        public string Verb { get; set; }

        /// <summary>
        /// Api path.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// To String.
        /// </summary>
        public override string ToString() {
            return string.Format("{0} {1}", Verb.ToUpper(), Uri);
        }

        /// <summary>
        /// Hash Code.
        /// </summary>
        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Equals.
        /// </summary>
        public override bool Equals(object obj) {
            ApiItem other = obj as ApiItem;
            if (other == null)
                return false;
            return object.Equals(this.Verb, other.Verb)
                && object.Equals(this.Uri, other.Uri);
        }

    }

}
