using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Models.Portal;

namespace Portal.Testing.Portal.Models {

    [TestClass]
    public class TestModelEquality {

        [TestMethod]
        public void Icon_Equals() {
            Icon a, b;
            a = new Icon();
            b = new Icon();
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals(a));
            a = PortalTestUtility.GetIcon();
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(b.Equals(a));
            b = PortalTestUtility.GetIcon();
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
            Assert.IsTrue(a.Equals(a));

            a.Id = 27;
            Assert.IsFalse(a.Equals(b));
            a = PortalTestUtility.GetIcon();

            a.Name = "Banana";
            Assert.IsFalse(a.Equals(b));
            a = PortalTestUtility.GetIcon();

            a.Image = "jpg";
            Assert.IsFalse(a.Equals(b));
            a = PortalTestUtility.GetIcon();

            a.Link = "bye.com";
            Assert.IsFalse(a.Equals(b));
            a = PortalTestUtility.GetIcon();

            a.DateCreated = DateTime.MinValue;
            Assert.IsTrue(a.Equals(b));
            a = PortalTestUtility.GetIcon();
        }

    }

}
