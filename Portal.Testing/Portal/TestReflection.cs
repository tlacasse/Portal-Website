using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Models.Portal;
using Portal.Website.Controllers.Portal;

namespace Portal.Testing.Portal {

    [TestClass]
    public class TestReflection {

        [TestMethod]
        public void GetApiAttributes() {
            IEnumerable<ApiItem> items = ApiViewController.ReflectApi(Assembly.GetExecutingAssembly());
            Assert.AreEqual(2, items.Count());
            ApiItem item;
            item = items.Where(i => i.Verb == "Get").Single();
            Assert.AreEqual("testing/prefix/gettest", item.Uri);
            item = items.Where(i => i.Verb == "Post").Single();
            Assert.AreEqual("testing/prefix/posttest", item.Uri);
        }

    }

}
