using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents a description record of a Grid change.
    /// </summary>
    public class GridChangeItem {

        /// <summary>
        /// The DateTime of the change.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// A description of the change.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// To String.
        /// </summary>
        public override string ToString() {
            return string.Format("{0} - {1}", DateTime, Event);
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
            GridChangeItem other = obj as GridChangeItem;
            if (other == null)
                return false;
            return object.Equals(this.DateTime, other.DateTime)
                && object.Equals(this.Event, other.Event);
        }

    }

}
