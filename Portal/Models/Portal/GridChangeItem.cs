using System;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents info about a change to the grid.
    /// </summary>
    public class GridChangeItem : IModel {

        /// <summary>
        /// The DateTime of the change.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// A small description of the change.
        /// </summary>
        public string Event { get; set; }

        public void ValidateData() {
            if (DateTime == null)
                throw new ArgumentNullException("DateTime");
            if (string.IsNullOrWhiteSpace(Event))
                throw new ArgumentNullException("Event");
        }

        public override string ToString() {
            return string.Format("{0} - {1}", DateTime, Event);
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj) {
            GridChangeItem other = obj as GridChangeItem;
            if (other == null)
                return false;
            return object.Equals(this.DateTime, other.DateTime)
                && object.Equals(this.Event, other.Event);
        }

    }

}
