using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortalTest {

    [TestClass]
    public class TestPortalUtility {

        [TestMethod]
        public void UrlFormat() {
            Assert.AreEqual("test", PortalUtility.UrlFormat("Test"));
            Assert.AreEqual("banana_muffins", PortalUtility.UrlFormat("Banana Muffins"));
            Assert.AreEqual("all_lower_case", PortalUtility.UrlFormat("All LOWER Case"));
        }

        [TestMethod]
        public void UnUrlFormat() {
            Assert.AreEqual("Test", PortalUtility.UnUrlFormat("test"));
            Assert.AreEqual("Banana Muffins", PortalUtility.UnUrlFormat("banana_muffins"));
            Assert.AreEqual("All Lower Case", PortalUtility.UnUrlFormat("all_lower_case"));
        }

    }

}
