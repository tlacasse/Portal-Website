using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Portal.Testing {

    [TestClass]
    public class TestPortalUtility {

        [TestMethod]
        public void PortalUtility_UrlFormat() {
            Assert.AreEqual("test", PortalUtility.UrlFormat("Test"));
            Assert.AreEqual("banana_muffins", PortalUtility.UrlFormat("Banana Muffins"));
            Assert.AreEqual("all_lower_case", PortalUtility.UrlFormat("All LOWER Case"));
        }

        [TestMethod]
        public void PortalUtility_UnUrlFormat() {
            Assert.AreEqual("Test", PortalUtility.UnUrlFormat("test"));
            Assert.AreEqual("Banana Muffins", PortalUtility.UnUrlFormat("banana_muffins"));
            Assert.AreEqual("All Lower Case", PortalUtility.UnUrlFormat("all_lower_case"));
        }

    }

}
