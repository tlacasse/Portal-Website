using Portal.Data.Models.Portal;
using Portal.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Portal.App.Portal.Messages {

    public class GridState {

        public GridSize Size { get; set; }
        public IEnumerable<IconPosition> Cells { get; set; }

        public override string ToString() {
            return string.Format("{0}x{1} ({2})",
                        Size.Width,
                        Size.Height,
                        Cells.Count());
        }

    }

}
