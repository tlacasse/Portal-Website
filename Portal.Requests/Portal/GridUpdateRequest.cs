using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Models.Portal;
using Portal.Requests.ConnectionExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Requests.Portal {

    public class GridUpdateRequest : DependentBase, IRequest<GridState, Void> {

        public GridUpdateRequest(IConnectionFactory ConnectionFactory, IWebsiteState WebsiteState)
            : base(ConnectionFactory, WebsiteState) {
        }

        public Void Process(GridState model) {
            model.ValidateData();
            using (IConnection connection = ConnectionFactory.Create()) {
                GridState current = connection.GetCurrentGridState(WebsiteState);
                IEnumerable<IconPosition> toBeInactive = GetIconsToBeInactive(current, model);
                if (toBeInactive.Any()) {
                    connection.ExecuteNonQuery(BuildInactivateQuery(toBeInactive), QueryOptions.Log);
                }
                IEnumerable<IconPosition> toBeAdded = GetIconsToBeAdded(current, model);
                if (toBeAdded.Any()) {
                    connection.ExecuteNonQuery(BuildAddIconsQuery(toBeAdded), QueryOptions.Log);
                }
            }
            WebsiteState.CurrentGridSize = model.Size;
            return null;
        }

        public IEnumerable<IconPosition> GetIconsToBeInactive(GridState currentGrid, GridState newGrid) {
            List<IconPosition> toBeInactive = new List<IconPosition>();
            foreach (IconPosition oldIcon in currentGrid.Cells) {
                // inactive if out of bounds
                if (oldIcon.XCoord >= newGrid.Size.Width) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                if (oldIcon.YCoord >= newGrid.Size.Height) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                // inactive if removed
                IEnumerable<IconPosition> newPositionMatches =
                    newGrid.Cells.Where(findNewIcon => oldIcon.PositionEquals(findNewIcon));
                if (newPositionMatches.Any() == false) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                // inactive if changed
                IconPosition newIcon = newPositionMatches.Single();
                if (oldIcon.Id != newIcon.Id) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
            }
            return toBeInactive;
        }

        public string BuildInactivateQuery(IEnumerable<IconPosition> toBeInactive) {
            DatabaseChangeQuery inactivateQuery = new DatabaseChangeQuery(QueryType.UPDATE, "PortalGrid");
            inactivateQuery.AddField("Active", "0", IsQuoted: false);
            inactivateQuery.AddField("DateUsed", PortalUtility.SqlTimestamp, IsQuoted: false);
            inactivateQuery.WhereClause = "WHERE Id IN ("
                + string.Join(",", toBeInactive.Select(ip => ip.GridId))
                + ")";
            return inactivateQuery.Build();
        }

        public IEnumerable<IconPosition> GetIconsToBeAdded(GridState currentGrid, GridState newGrid) {
            // add any where position or id changed
            return newGrid.Cells.Where(
                newIcon => currentGrid.Cells.Where(
                    oldIcon => oldIcon.PositionEquals(newIcon) && oldIcon.Id == newIcon.Id
                ).Any() == false
            );
        }

        public string BuildAddIconsQuery(IEnumerable<IconPosition> toBeAdded) {
            DatabaseChangeQuery addQuery = new DatabaseChangeQuery(QueryType.INSERT, "PortalGrid");
            addQuery.AddField("Icon", toBeAdded.Select(icon => icon.Id.ToString()), IsQuoted: false);
            addQuery.AddField("XCoord", toBeAdded.Select(icon => icon.XCoord.ToString()), IsQuoted: false);
            addQuery.AddField("YCoord", toBeAdded.Select(icon => icon.YCoord.ToString()), IsQuoted: false);
            addQuery.AddField("DateUsed", toBeAdded.Select(_ => PortalUtility.SqlTimestamp), IsQuoted: false);
            addQuery.AddField("Active", toBeAdded.Select(_ => "1"), IsQuoted: false);
            return addQuery.Build();
        }

    }

}
