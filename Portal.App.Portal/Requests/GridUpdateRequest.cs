using Portal.App.Portal.Messages;
using Portal.App.Portal.Services;
using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Data.Web;
using Portal.Structure;
using Portal.Structure.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class GridUpdateRequest : CommonDependent2, IRequestIn<GridState> {

        private IIconValidatorService IconValidatorService { get; }

        public GridUpdateRequest(IConnectionFactory ConnectionFactory,
                IWebsiteState WebsiteState, IIconValidatorService IconValidatorService)
                : base(ConnectionFactory, WebsiteState) {
            this.IconValidatorService = IconValidatorService;
        }

        public void Process(GridState model) {
            GridState newGrid = model;
            IconValidatorService.ValidateIconGridState(newGrid);

            using (IConnection connection = ConnectionFactory.Create()) {
                GridState oldGrid = BuildCurrentGridState(connection);

                int changes = 0;
                foreach (IconPosition icon in BuildIconsToBeInactive(newGrid, oldGrid)) {
                    icon.IsActive = false;
                    changes++;
                }
                foreach (IconPosition icon in BuildIconsToBeAdded(newGrid, oldGrid)) {
                    // coords from client
                    icon.IsActive = true;
                    icon.DateUpdated = DateTime.Now;
                    icon.Icon = connection.IconById(icon.Icon.Id);
                    connection.IconPositionTable.Add(icon);
                    changes++;
                }

                connection.Log("Grid Build",
                    string.Format("{0}x{1} ({2}) (d{3})",
                        newGrid.Size.Width,
                        newGrid.Size.Height,
                        newGrid.Cells.Count(),
                        changes));

                connection.SaveChanges();
            }

            WebsiteState.ActiveIconGridSize = model.Size;
        }

        private GridState BuildCurrentGridState(IConnection connection) {
            GridState oldGrid = new GridState() {
                Size = WebsiteState.ActiveIconGridSize
            };
            oldGrid.Cells = connection.ActiveGridIcons();
            return oldGrid;
        }

        private IEnumerable<IconPosition> BuildIconsToBeInactive(GridState newGrid, GridState oldGrid) {
            List<IconPosition> toBeInactive = new List<IconPosition>();
            foreach (IconPosition oldIcon in oldGrid.Cells) {
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
                if (newPositionMatches.NotAny()) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
                // inactive if changed
                IconPosition newIcon = newPositionMatches.Single();
                if (oldIcon.Icon.Id != newIcon.Icon.Id) {
                    toBeInactive.Add(oldIcon);
                    continue;
                }
            }
            return toBeInactive;
        }

        private IEnumerable<IconPosition> BuildIconsToBeAdded(GridState newGrid, GridState oldGrid) {
            // add any where position or id changed
            return newGrid.Cells.Where(
                newIcon => oldGrid.Cells.Where(
                    oldIcon => oldIcon.PositionEquals(newIcon) && oldIcon.Icon.Id == newIcon.Icon.Id
                ).NotAny()
            );
        }

    }

}
