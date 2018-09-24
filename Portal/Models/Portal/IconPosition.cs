using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents an Icon at a certain position in the grid.
    /// </summary>
    public class IconPosition : Icon {

        /// <summary>
        /// Zero-based horizontal position from the left of the grid.
        /// </summary>
        public int XCoord { get; set; } = -1;

        /// <summary>
        /// Zero-based vertical position from the top of the grid.
        /// </summary>
        public int YCoord { get; set; } = -1;

        /// <summary>
        /// Identifier for a Grid record.
        /// </summary>
        public int GridId { get; set; } = -1;

        /// <summary>
        /// DateTime when the 
        /// </summary>
        public DateTime DateUsed { get; set; }

        /// <summary>
        /// To String.
        /// </summary>
        public override string ToString() {
            return string.Format("{0}x{1} ({2})", XCoord, YCoord, base.ToString());
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
            if (base.Equals(obj)) {
                IconPosition other = obj as IconPosition;
                if (other == null)
                    return false;
                return this.PositionEquals(other);
            }
            return false;
        }

        /// <summary>
        /// Returns true if the coordinates are equal, not considering the actual icon.
        /// </summary>
        public bool PositionEquals(IconPosition other) {
            return this.XCoord == other.XCoord
                && this.YCoord == other.YCoord;
        }

        /// <summary>
        /// Throws an exception if any properties are invalid or not allowed. Prefer ValidateData(GridSize).
        /// </summary>
        public override void ValidateData() {
            base.ValidateData();
            if (XCoord < 0)
                throw new ArgumentOutOfRangeException("XCoord");
            if (YCoord < 0)
                throw new ArgumentOutOfRangeException("YCoord");
        }

        /// <summary>
        /// Throws an exception if any properties are invalid or not allowed.
        /// </summary>
        public void ValidateData(GridSize currentGridSize) {
            ValidateData();
            if (XCoord > currentGridSize.Width - 1)
                throw new ArgumentOutOfRangeException("XCoord");
            if (YCoord > currentGridSize.Height - 1)
                throw new ArgumentOutOfRangeException("YCoord");
        }

        /// <summary>
        /// Not allowed on IconPosition.
        /// </summary>
        public override string BuildUpdateQuery() {
            throw new InvalidOperationException();
        }

    }

}
