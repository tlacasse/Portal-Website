using Newtonsoft.Json;
using System;
using System.Configuration;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents the dimensions of a grid.
    /// </summary>
    public class GridSize : IModel {

        private static readonly int MIN = int.Parse(ConfigurationManager.AppSettings["gridMin"]);
        private static readonly int MAX = int.Parse(ConfigurationManager.AppSettings["gridMax"]);

        /// <summary>
        /// Number of cells horizontally.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Number of cells vertically.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Minimum number of cells in either direction.
        /// </summary>
        [JsonIgnore]
        public int Min {
            get { return MIN; }
        }

        /// <summary>
        /// Maximum number of cells in either direction.
        /// </summary>
        [JsonIgnore]
        public int Max {
            get { return MAX; }
        }

        public void ValidateData() {
            if (Width > Max)
                throw new ArgumentOutOfRangeException("Width", string.Format("Maximum width is {0}.", Max));
            if (Height < Min)
                throw new ArgumentOutOfRangeException("Height", string.Format("Minimum width is {0}.", Min));
            if (Width < Height)
                throw new ArgumentOutOfRangeException("Width", "Width must be greater than Height.");
        }

        public override string ToString() {
            return string.Format("{0}x{1}", Width, Height);
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj) {
            GridSize other = obj as GridSize;
            if (other == null)
                return false;
            return this.Width == other.Width
                && this.Height == other.Height;
        }

    }

}
