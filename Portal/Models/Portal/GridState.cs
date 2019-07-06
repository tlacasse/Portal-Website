using System.Collections.Generic;
using System.Linq;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents a specific size grid and the Icons that populate it.
    /// </summary>
    public class GridState : IModel {

        /// <summary>
        /// The dimensions of the grid.
        /// </summary>
        public GridSize Size { get; set; }

        /// <summary>
        /// A list of positioned Icons that populate the grid.
        /// </summary>
        public IEnumerable<IconPosition> Cells { get; set; }

        public void ValidateData() {
            Size.ValidateData();
            foreach (IconPosition icon in Cells) {
                icon.ValidateData(Size);
            }
        }

        public override string ToString() {
            return string.Format("{0} ({1})", Size.ToString(), Cells == null ? 0 : Cells.Count());
        }

        public override int GetHashCode() {
            return Size.GetHashCode() * (Cells == null ? 1 :
                Cells.Aggregate(31, (a, b) => a.GetHashCode() * b.GetHashCode())
            );
        }

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
