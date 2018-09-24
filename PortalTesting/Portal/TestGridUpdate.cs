using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Models.Portal;
using Portal.Models.Portal.Specific;
using PortalWebsite.Data.Logic.Portal;

namespace PortalTesting.Portal {

    [TestClass]
    public class TestGridUpdate {

        private static GridState NewGrid { get; set; }
        private static GridState OldGrid { get; set; }

        [ClassInitialize]
        public static void Init(TestContext context) {
            OldGrid = new GridState {
                Size = new GridSize() {
                    Width = 10,
                    Height = 6
                },
                Cells = new List<IconPosition>() {
                    new IconPosition() {
                        Name = "Removed because out of range",
                        XCoord = 8,
                        YCoord = 2,
                        Id = 1,
                        GridId = 10
                    },
                    new IconPosition() {
                        Name = "In Both",
                        XCoord = 1,
                        YCoord = 2,
                        Id = 2,
                        GridId = 20
                    },
                    new IconPosition() {
                        Name = "Same position different Id",
                        XCoord = 3,
                        YCoord = 2,
                        Id = 3,
                        GridId = 30
                    },
                    new IconPosition() {
                        Name = "Different position same Id",
                        XCoord = 4,
                        YCoord = 2,
                        Id = 4,
                        GridId = 40
                    }
                }
            };

            NewGrid = new GridState {
                Size = new GridSize() {
                    Width = 6,
                    Height = 4
                },
                Cells = new List<IconPosition>() {
                    new IconPosition() {
                        Name = "In Both",
                        XCoord = 1,
                        YCoord = 2,
                        Id = 2
                    },
                    new IconPosition() {
                        Name = "Same position different Id",
                        XCoord = 3,
                        YCoord = 2,
                        Id = 5
                    },
                    new IconPosition() {
                        Name = "Different position same Id",
                        XCoord = 1,
                        YCoord = 0,
                        Id = 4
                    }
                }
            };
        }

        [TestMethod]
        public void GridUpdate_RemovedIcons() {
            IEnumerable<IconPosition> toBeInactive = OldGrid.GetIconsToBeInactive(NewGrid);
            Assert.AreEqual(3, toBeInactive.Count());
            foreach (int gridId in new int[] { 10, 30, 40 }) {
                toBeInactive.Where(i => i.GridId == gridId).Single();
            }
        }

        [TestMethod]
        public void GridUpdate_AddedIcons() {
            IEnumerable<IconPosition> toBeAdded = NewGrid.GetIconsToBeAdded(OldGrid);
            Assert.AreEqual(2, toBeAdded.Count());
            foreach (int iconId in new int[] { 4, 5 }) {
                toBeAdded.Where(i => i.Id == iconId).Single();
            }
        }

    }

}
