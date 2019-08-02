using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Tests.Fakes;
using System;

namespace Portal.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsInvalid : TestBase {

        private IconUploadRequest request;

        [TestInitialize]
        public void Setup() {
            request = new IconUploadRequest(WebsiteState,
                new FakeDatabaseFactory(new FakeDatabase()),
                new FakeFileReceiver());
        }

        [TestMethod]
        public void FailOnEmptyIcon() {
            Assert.ThrowsException<ArgumentNullException>(
                () => request.Process(new Icon())
            );
        }

        [TestMethod]
        public void FailOnInvalidIconName() {
            Assert.ThrowsException<ArgumentException>(
                () => request.Process(new Icon() {
                    Name = "inv@l!d ",
                    Link = "a.com",
                    Image = "png"
                })
            );
        }

    }

}
