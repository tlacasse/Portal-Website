using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Models.Portal;

namespace PortalTesting.Portal {

    [TestClass]
    public class TestGridSizeModel {

        [TestMethod]
        public void GridSize_Equals() {
            GridSize a = new GridSize();
            GridSize b = new GridSize();
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));

            a.Width = 17;
            Assert.IsFalse(a.Equals(b));
            b.Width = 17;
            Assert.IsTrue(a.Equals(b));

            a.Height = 10;
            Assert.IsFalse(a.Equals(b));
            b.Height = 10;
            Assert.IsTrue(a.Equals(b));

            Assert.IsTrue(b.Equals(a));
            Assert.IsTrue(a.Equals(a));
        }

        public GridSize GetGoodGridSize() {
            return new GridSize() {
                Width = 17,
                Height = 9
            };
        }

        [TestMethod]
        public void GridSizeValidation_Nothing() {
            GridSize size = new GridSize();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => size.ValidateData()
            );
        }

        [TestMethod]
        public void GridSizeValidation_HighWidth() {
            GridSize size = GetGoodGridSize();
            size.Width = 5000;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => size.ValidateData()
            );
        }

        [TestMethod]
        public void GridSizeValidation_LowHeight() {
            GridSize size = GetGoodGridSize();
            size.Height = 2;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => size.ValidateData()
            );
        }

        [TestMethod]
        public void GridSizeValidation_UnbalancedWidth() {
            GridSize size = GetGoodGridSize();
            size.Width = 5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => size.ValidateData()
            );
        }

        [TestMethod]
        public void GridSizeValidation_UnbalancedHeight() {
            GridSize size = GetGoodGridSize();
            size.Height = 26;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => size.ValidateData()
            );
        }

        [TestMethod]
        public void GridSizeValidation_Good() {
            GridSize size = GetGoodGridSize();
            size.ValidateData();
        }

    }

}
