using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Data.Web;

namespace Portal.Tests.Framework.TestSettings {

    [TestClass]
    public class WithSelectedSettings {

        [TestMethod]
        public void NonReadOnlyShouldNotBeReadOnly() {
            Assert.IsFalse(Setting.PortalGridCurrentHeight.IsReadonlySetting());
        }

        [TestMethod]
        public void ReadOnlyShouldBeReadOnly() {
            Assert.IsTrue(Setting.PortalGridMaxSize.IsReadonlySetting());
        }

    }

}
