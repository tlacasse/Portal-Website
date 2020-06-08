using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Data.Models.Portal;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconHasDuplicateName : TestBase {

        private IconUploadRequest request;
        private IconPost post;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            request = new IconUploadRequest(cf,
                IconService, WebsiteState,
                new FakeFileReceiver(1, "Test File"));

            Icon existingIcon = new Icon() {
                Id = 5,
                Name = "Test Icon",
                Image = "png",
                Link = "a.com/b"
            };
            cf.IconsList.Init(existingIcon);

            Icon icon = new Icon() {
                Name = "Test Icon",
                Image = "png",
                Link = "a.com"
            };
            post = new IconPost() {
                Id = -1,
                Name = "Test Icon",
                Link = "a.com"
            };
        }

        [TestMethod]
        public void FailOnDuplicateName() {
            ExpectPortalException(() => request.Process(post), IconResult.FAIL_ALREADY_EXISTS);
        }

    }
}
