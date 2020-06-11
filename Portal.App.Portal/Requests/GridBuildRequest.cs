using Portal.App.Portal.Messages;
using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Web;
using Portal.Structure;
using Portal.Structure.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Portal.App.Portal.Requests {

    public class GridBuildRequest : CommonDependent2, IRequestEvent {

        public GridBuildRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        private static readonly string GRID_INJECT_LOCATION = "<!-- Grid -->";

        private static readonly string GRID_STYLE_FORMAT
            = "background-image: url(/Data/Icons/{0}.{1}?{2}); "
            + "background-size: contain; "
            + "background-repeat: no-repeat; "
            + "background-position: center; ";

        private static readonly string ON_DBLCLICK_FORMAT = "goToLink('{0}');";

        private static readonly XAttribute CELL_CLASS_ATTRIBUTE
            = new XAttribute("class", "cell");

        private static readonly XAttribute ON_CLICK_FORMAT
            = new XAttribute("onclick", "highlight(this);");

        public void Process() {
            GridState grid;
            using (IConnection connection = ConnectionFactory.Create()) {
                grid = connection.BuildCurrentGridState(WebsiteState);

                string gridHtml = BuildGridHTML(grid);
                File.WriteAllText(WebsiteState.IndexBuiltPath,
                    File.ReadAllText(WebsiteState.IndexEmptyPath).Replace(GRID_INJECT_LOCATION, gridHtml)
                );

                WebsiteState.LastGridBuildTime = DateTime.Now;
                connection.Log("Grid Build", grid.ToString());
            }
        }

        private string BuildGridHTML(GridState grid) {
            List<XElement> rows = new List<XElement>();
            for (int y = 0; y < grid.Size.Height; y++) {
                List<XElement> cells = new List<XElement>();
                for (int x = 0; x < grid.Size.Width; x++) {
                    cells.Add(new XElement("td", CreateCell(x, y, grid)));
                }
                rows.Add(new XElement("tr", cells));
            }
            return (new XElement("table", rows)).ToString();
        }

        private XElement CreateCell(int x, int y, GridState grid) {
            IconPosition test = new IconPosition() { XCoord = x, YCoord = y };
            Icon icon = grid.Cells.Where(ip => ip.PositionEquals(test)).FirstOrDefault()?.Icon;
            XElement div;
            if (icon != null) {
                XAttribute styleAttribute = new XAttribute("style",
                    string.Format(GRID_STYLE_FORMAT, icon.Id, icon.Image,
                    icon.DateChanged.ToString().Replace(" ", ""))
                );
                XAttribute ondblclickAttribute = new XAttribute("ondblclick",
                    string.Format(ON_DBLCLICK_FORMAT, icon.Link)
                );
                div = new XElement("div", CELL_CLASS_ATTRIBUTE, ON_CLICK_FORMAT,
                    styleAttribute, ondblclickAttribute);
            } else {
                div = new XElement("div");
            }
            return div;
        }

    }

}
