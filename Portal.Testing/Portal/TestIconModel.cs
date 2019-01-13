using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Models.Portal;

namespace Portal.Testing.Portal {

    [TestClass]
    public class TestIconModel {

        [TestMethod]
        public void Icon_IsNew() {
            Icon icon = new Icon();
            Assert.IsTrue(icon.IsNew);
            icon.Id = 267;
            Assert.IsFalse(icon.IsNew);
        }

        [TestMethod]
        public void Icon_Equals() {
            Icon a = new Icon();
            Icon b = new Icon();
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));

            a.Name = "Test";
            Assert.IsFalse(a.Equals(b));
            b.Name = "Test";
            Assert.IsTrue(a.Equals(b));

            a.Link = "a@a.com";
            Assert.IsFalse(a.Equals(b));
            b.Link = "a@a.com";
            Assert.IsTrue(a.Equals(b));

            a.Image = "png";
            Assert.IsFalse(a.Equals(b));
            b.Image = "png";
            Assert.IsTrue(a.Equals(b));

            a.Id = 5;
            Assert.IsFalse(a.Equals(b));
            b.Id = 5;
            Assert.IsTrue(a.Equals(b));

            a.Name = "Apple";
            Assert.IsFalse(a.Equals(b));
            b.Name = "Apple";
            Assert.IsTrue(a.Equals(b));

            a.Link = "b@b.com";
            Assert.IsFalse(a.Equals(b));
            b.Link = "b@b.com";
            Assert.IsTrue(a.Equals(b));

            a.Image = "jpg";
            Assert.IsFalse(a.Equals(b));
            b.Image = "jpg";
            Assert.IsTrue(a.Equals(b));

            a.Id = 27;
            Assert.IsFalse(a.Equals(b));
            b.Id = 27;
            Assert.IsTrue(a.Equals(b));

            a.DateChanged = new DateTime();
            Assert.IsTrue(a.Equals(b));

            Assert.IsTrue(b.Equals(a));
            Assert.IsTrue(a.Equals(a));
            Assert.IsTrue(b.Equals(b));
        }

    }

}
