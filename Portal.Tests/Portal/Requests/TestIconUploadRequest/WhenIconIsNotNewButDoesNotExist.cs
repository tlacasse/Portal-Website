using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Tests.Fakes;

namespace Portal.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsNotNewButDoesNotExist : TestBase {

        private IconUploadRequest request;
        private Icon icon;

        [TestInitialize]
        public void Setup() {
            request = new IconUploadRequest(WebsiteState,
               new FakeDatabaseFactory(new FakeDatabase()),
               new FakeEmptyFileReceiver());

            icon = new Icon() {
                Id = 5,
                Name = "Test Icon",
                Image = "png",
                Link = "a.com"
            };
        }

        [TestMethod]
        public void FailOnMissingFile() {
            Assert.ThrowsException<PortalException>(
                () => request.Process(icon)
            );
        }

    }

}
