using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;

namespace Portal.Requests.Portal {

    public class BuildGridRequest : DependentBase, IRequest<Void, Void> {

        private static string GridInjectLocation { get; } = "<!-- Grid -->";

        private static string GridStyleFormat { get; }
            = "background-image: url(/Portal/Icons/{0}.{1}?{2}); "
            + "background-size: contain; "
            + "background-repeat: no-repeat; "
            + "background-position: center; ";

        private static string OnDblClickFormat { get; } = "goToLink('{0}');";

        private static XAttribute ClassAttribute { get; }
            = new XAttribute("class", "cell");

        private static XAttribute OnclickAttribute { get; }
            = new XAttribute("onclick", "highlight(this);");

        public BuildGridRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public Void Process(Void model) {
            GridState grid;
            using (IConnection connection = ConnectionFactory.Create()) {
                grid = connection.GetCurrentGridState(WebsiteState);
                connection.Log("Grid Build", grid.ToString());
            }
            string gridHtml = BuildGridHTML(grid);
            File.WriteAllText(WebsiteState.IndexPath,
                File.ReadAllText(WebsiteState.IndexEmptyPath).Replace(GridInjectLocation, gridHtml)
            );
            WebsiteState.LastGridBuildTime = DateTime.Now;
            return null;
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
            Icon icon = grid.Cells.Where(ip => ip.PositionEquals(test)).FirstOrDefault();
            XElement div;
            if (icon != null) {
                XAttribute styleAttribute = new XAttribute("style",
                    string.Format(GridStyleFormat, icon.Id, icon.Image, icon.DateChanged)
                );
                XAttribute ondblclickAttribute = new XAttribute("ondblclick",
                    string.Format(OnDblClickFormat, icon.Link)
                );
                div = new XElement("div", ClassAttribute, OnclickAttribute,
                    styleAttribute, ondblclickAttribute);
            } else {
                div = new XElement("div");
            }
            return div;
        }

    }

}
