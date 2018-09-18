using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    public class GridSize {

        private static readonly int MIN = 4;
        private static readonly int MAX = 30;

        /// <summary>
        /// The number of Icons allowed horizontally.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The number of Icons allowed vertically.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The minimum number of Icons in either direction.
        /// </summary>
        [JsonIgnore]
        public int Min {
            get { return MIN; }
        }

        /// <summary>
        /// The maximum number of Icons in either direction.
        /// </summary>
        [JsonIgnore]
        public int Max {
            get { return MAX; }
        }

        /// <summary>
        /// Throws an exception if any properties are invalid or not allowed.
        /// </summary>
        public void ValidateData() {
            if (Width > Max)
                throw new ArgumentOutOfRangeException("Width", string.Format("Maximum width is {0}.", Max));
            if (Height < Min)
                throw new ArgumentOutOfRangeException("Height", string.Format("Minimum width is {0}.", Min));
            if (Width < Height)
                throw new ArgumentOutOfRangeException("Width", "Width must be greater than Height.");
        }

        /// <summary>
        /// To String.
        /// </summary>
        public override string ToString() {
            return string.Format("{0}x{1}", Width, Height);
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
            GridSize other = obj as GridSize;
            if (other == null)
                return false;
            return this.Width == other.Width
                && this.Height == other.Height;
        }

    }

}
