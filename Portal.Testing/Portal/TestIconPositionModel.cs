using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Models.Portal;

namespace Portal.Testing.Portal {

    [TestClass]
    public class TestIconPositionModel {

        [TestMethod]
        public void IconPosition_Equals() {
            IconPosition a = new IconPosition();
            IconPosition b = new IconPosition();
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));

            a.Name = "Banana";
            Assert.IsFalse(a.Equals(b));
            b.Name = "Banana";
            Assert.IsTrue(a.Equals(b));

            a.Id = 5;
            b.Id = 5;
            a.Image = "png";
            b.Image = "png";
            a.Link = "web.com";
            b.Link = "web.com";
            Assert.IsTrue(a.Equals(b));

            a.XCoord = 7;
            Assert.IsFalse(a.Equals(b));
            b.XCoord = 7;
            Assert.IsTrue(a.Equals(b));

            a.YCoord = 7;
            Assert.IsFalse(a.Equals(b));
            b.YCoord = 7;
            Assert.IsTrue(a.Equals(b));

            a.DateUsed = new DateTime();
            Assert.IsTrue(a.Equals(a));
            Assert.IsTrue(b.Equals(b));
        }

        private GridSize GetGridSize() {
            return new GridSize() {
                Width = 10,
                Height = 6
            };
        }

        private IconPosition GetGoodIconPosition() {
            return new IconPosition() {
                Id = 4,
                Name = "Example",
                Link = "website.org",
                Image = "png",
                XCoord = 8,
                YCoord = 5
            };
        }

        [TestMethod]
        public void IconPositionValidation_Nothing() {
            IconPosition icon = new IconPosition();
            Assert.ThrowsException<ArgumentNullException>(
                () => icon.ValidateData()
            );
            Assert.ThrowsException<ArgumentNullException>(
                () => icon.ValidateData(GetGridSize())
            );
        }

        [TestMethod]
        public void IconPositionValidation_IconTest() {
            IconPosition icon = GetGoodIconPosition();
            icon.Name = "*%(#*Y&%";
            Assert.ThrowsException<ArgumentException>(
                () => icon.ValidateData()
            );
            Assert.ThrowsException<ArgumentException>(
                () => icon.ValidateData(GetGridSize())
            );
        }

        [TestMethod]
        public void IconPositionValidation_XCoordBelow() {
            IconPosition icon = GetGoodIconPosition();
            icon.XCoord = -5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => icon.ValidateData()
            );
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => icon.ValidateData(GetGridSize())
            );
        }

        [TestMethod]
        public void IconPositionValidation_YCoordBelow() {
            IconPosition icon = GetGoodIconPosition();
            icon.YCoord = -5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => icon.ValidateData()
            );
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => icon.ValidateData(GetGridSize())
            );
        }

        [TestMethod]
        public void IconPositionValidation_XCoordAbove() {
            IconPosition icon = GetGoodIconPosition();
            icon.XCoord = 15;
            icon.ValidateData();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => icon.ValidateData(GetGridSize())
            );
        }

        [TestMethod]
        public void IconPositionValidation_YCoordAbove() {
            IconPosition icon = GetGoodIconPosition();
            icon.YCoord = 8;
            icon.ValidateData();
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => icon.ValidateData(GetGridSize())
            );
        }

        [TestMethod]
        public void IconPositionValidation_Good() {
            IconPosition icon = GetGoodIconPosition();
            icon.ValidateData();
        }

    }

}
