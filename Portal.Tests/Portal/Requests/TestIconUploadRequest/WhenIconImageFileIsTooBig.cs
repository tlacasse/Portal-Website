using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Tests.Fakes;
using System;

namespace Portal.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconImageFileIsTooBig : TestBase {

        private IconUploadRequest request;
        private Icon icon;

        [TestInitialize]
        public void Setup() {
            request = new IconUploadRequest(WebsiteState,
               new FakeDatabaseFactory(new FakeDatabase()),
               new FakeFileReceiver(1000000, "test"));

            icon = new Icon() {
                Name = "test icon",
                Image = "png",
                Link = "a.com"
            };
        }

        [TestMethod]
        public void FailOnMissingFile() {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => request.Process(icon)
            );
        }

    }

}
