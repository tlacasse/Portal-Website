using System;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents a clickable icon at a certain coordinate.
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
        /// Identifier for a Grid record. Not equal to Icon Id.
        /// </summary>
        public int GridId { get; set; } = -1;

        /// <summary>
        /// DateTime when the Icon was placed on the grid.
        /// </summary>
        public DateTime DateUsed { get; set; }

        /// <summary>
        /// Returns true if the coordinates are equal, not considering the actual icon.
        /// </summary>
        public bool PositionEquals(IconPosition other) {
            return this.XCoord == other.XCoord
                && this.YCoord == other.YCoord;
        }

        public override void ValidateData() {
            base.ValidateData();
            if (XCoord < 0)
                throw new ArgumentOutOfRangeException("XCoord");
            if (YCoord < 0)
                throw new ArgumentOutOfRangeException("YCoord");
        }

        public void ValidateData(GridSize currentGridSize) {
            ValidateData();
            if (XCoord > currentGridSize.Width - 1)
                throw new ArgumentOutOfRangeException("XCoord");
            if (YCoord > currentGridSize.Height - 1)
                throw new ArgumentOutOfRangeException("YCoord");
        }

        public override string ToString() {
            return string.Format("{0}x{1} ({2})", XCoord, YCoord, base.ToString());
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj) {
            if (base.Equals(obj)) {
                IconPosition other = obj as IconPosition;
                if (other == null)
                    return false;
                return this.PositionEquals(other);
            }
            return false;
        }

    }

}
