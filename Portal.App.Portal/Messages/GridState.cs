using Portal.Data.Models.Portal;
using Portal.Messages;
using System.Collections.Generic;

namespace Portal.App.Portal.Messages {

    public class GridState {

        public GridSize Size { get; set; }
        public IEnumerable<IconPosition> Cells { get; set; }

    }

}
