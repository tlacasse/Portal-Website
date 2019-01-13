﻿using Newtonsoft.Json;
using Portal;
using Portal.Data;
using Portal.Data.Web;
using Portal.Models.Portal;
using PortalWebsite.Controllers.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalWebsite.Data.Logic.Portal {

    public static class GridExtensions {

        /// <summary>
        /// Saves the JSON of a GridSize to the specified path.
        /// </summary>
        public static void SaveSizeToPath(this GridSize size, string path) {
            string json = JsonConvert.SerializeObject(size);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Saves the JSON of a GridSize to the location for the Website.
        /// </summary>
        public static void SaveSize(this GridSize size) {
            size.SaveSizeToPath(GridController.GRID_DIMENSIONS_FILE);
        }

        /// <summary>
        /// Compares an old Grid to a new Grid, to determine which of the old Icons should be removed.
        /// </summary>
        public static IEnumerable<IconPosition> GetIconsToBeInactive(this GridState current, GridState newGrid) {
            List<IconPosition> toBeInactive = new List<IconPosition>();
            foreach (IconPosition oldIcon in current.Cells) {
                if (oldIcon.XCoord >= newGrid.Size.Width) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                if (oldIcon.YCoord >= newGrid.Size.Height) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                IEnumerable<IconPosition> newPositionMatches =
                    newGrid.Cells.Where(findNewIcon => oldIcon.PositionEquals(findNewIcon));
                if (newPositionMatches.Any() == false) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                IconPosition newIcon = newPositionMatches.Single();
                if (oldIcon.Id != newIcon.Id) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
            }
            return toBeInactive;
        }

        /// <summary>
        /// Takes a list of IconPositions and will create a query that will set them all to inactive.
        /// </summary>
        public static string BuildInactivateQuery(this IEnumerable<IconPosition> toBeInactive) {
            DatabaseUpdateQuery inactivateQuery = new DatabaseUpdateQuery(DatabaseUpdateQuery.QueryType.UPDATE, "PortalGrid");
            inactivateQuery.AddField("Active", "0", false);
            inactivateQuery.AddField("DateUsed", PortalUtility.SqlTimestamp, false);
            inactivateQuery.WhereClause = "WHERE Id IN ("
                + string.Join(",", toBeInactive.Select(ip => ip.GridId))
                + ")";
            return inactivateQuery.Build();
        }

        /// <summary>
        /// Compares a new Grid to an old Grid, determining which Icons are actually new.
        /// </summary>
        public static IEnumerable<IconPosition> GetIconsToBeAdded(this GridState newGrid, GridState oldGrid) {
            return newGrid.Cells.Where(
                newIcon => oldGrid.Cells.Where(
                    oldIcon => oldIcon.PositionEquals(newIcon) && oldIcon.Id == newIcon.Id
                ).Any() == false
            );
        }

        /// <summary>
        /// Takes a list of IconPositions and will create a query that will add them to the Grid table.
        /// </summary>
        public static string BuildAddIconsQuery(this IEnumerable<IconPosition> toBeAdded) {
            DatabaseUpdateQuery addQuery = new DatabaseUpdateQuery(DatabaseUpdateQuery.QueryType.INSERT, "PortalGrid");
            addQuery.AddField("Icon", toBeAdded.Select(icon => icon.Id.ToString()), false);
            addQuery.AddField("XCoord", toBeAdded.Select(icon => icon.XCoord.ToString()), false);
            addQuery.AddField("YCoord", toBeAdded.Select(icon => icon.YCoord.ToString()), false);
            addQuery.AddField("DateUsed", toBeAdded.Select(_ => PortalUtility.SqlTimestamp), false);
            addQuery.AddField("Active", toBeAdded.Select(_ => "1"), false);
            return addQuery.Build();
        }

        private static readonly string GRID_STYLE_FORMAT =
            "background-image: url(/Portal/Icons/{0}.{1}?{2}); "
            + "background-size: contain; "
            + "background-repeat: no-repeat; "
            + "background-position: center; ";

        /// <summary>
        /// Builds the HTML of an Icon Grid.
        /// </summary>
        public static string BuildGridHTML(this GridState grid) {
            HtmlBuilder builder = new HtmlBuilder();
            for (int y = 0; y < grid.Size.Height; y++) {
                builder.Tag("tr").Start();
                for (int x = 0; x < grid.Size.Width; x++) {
                    IconPosition test = new IconPosition() { XCoord = x, YCoord = y };
                    Icon icon = grid.Cells.Where(ip => ip.PositionEquals(test)).FirstOrDefault();
                    builder.Tag("td").Start();
                    if (icon != null) {
                        builder
                            .Tag("div")
                                .Attribute("style", string.Format(GRID_STYLE_FORMAT, icon.Id, icon.Image, icon.DateChanged))
                                .Attribute("ondblclick", string.Format("goToLink('{0}');", icon.Link))
                                .Attribute("onclick", "highlight(this);")
                                .Attribute("class", "cell")
                            .Start()
                            .End();
                    }
                    builder.End();
                }
                builder.End();
            }
            return builder.Finish();
        }

    }

}