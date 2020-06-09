using System.Configuration;

namespace Portal.Messages {

    public class GridSize {

        private static readonly int MIN = int.Parse(ConfigurationManager.AppSettings["gridMin"]);
        private static readonly int MAX = int.Parse(ConfigurationManager.AppSettings["gridMax"]);

        public int Width { get; set; }

        public int Height { get; set; }

        public int Min {
            get { return MIN; }
        }

        public int Max {
            get { return MAX; }
        }

        public static GridSize BuildDefault() {
            return new GridSize() {
                Width = int.Parse(ConfigurationManager.AppSettings["gridWidthDefault"]),
                Height = int.Parse(ConfigurationManager.AppSettings["gridHeightDefault"])
            };
        }

    }

}
