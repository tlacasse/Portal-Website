using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    /// <summary>
    /// Relevant data for the current state of a grid.
    /// </summary>
    public class GridState {

        /// <summary>
        /// The dimensions of the grid.
        /// </summary>
        public GridSize Size { get; set; }

        /// <summary>
        /// Each of the icons and their positions.
        /// </summary>
        public IEnumerable<IconPosition> Cells { get; set; }

        /// <summary>
        /// Throws an exception if any properties are invalid or not allowed.
        /// </summary>
        public void ValidateData() {
            foreach (IconPosition icon in Cells) {
                icon.ValidateData(Size);
            }
        }

        /// <summary>
        /// To String.
        /// </summary>
        public override string ToString() {
            return string.Format("{0} ({1})", Size.ToString(), Cells == null ? 0 : Cells.Count());
        }

        /// <summary>
        /// Hash Code.
        /// </summary>
        public override int GetHashCode() {
            return Size.GetHashCode() * (Cells == null ? 1 :
                Cells.Aggregate(31, (a, b) => a.GetHashCode() * b.GetHashCode())
            );
        }

        /// <summary>
        /// Equals.
        /// </summary>
        public override bool Equals(object obj) {
            GridState other = obj as GridState;
            if (other == null)
                return false;
            if (this.Size.Equals(other.Size) == false)
                return false;
            if ((this.Cells == null) != (other.Cells == null))
                return false;
            if (this.Cells == null)
                return true;
            if (this.Cells.Count() != other.Cells.Count())
                return false;
            foreach (IconPosition icon in this.Cells) {
                if (other.Cells.Contains(icon) == false)
                    return false;
            }
            return true;
        }

    }

}
