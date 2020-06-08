using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsNotNewButDoesNotExist : TestBase {

        private IconUploadRequest request;
        private IconPost post;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            request = new IconUploadRequest(cf,
                IconService, WebsiteState,
                new FakeEmptyFileReceiver());

            post = new IconPost() {
                Id = 5,
                Name = "Test Icon",
                Link = "a.com/b"
            };
        }

        [TestMethod]
        public void FailOnTooLargeFile() {
            ExpectPortalException(() => request.Process(post), IconResult.FAIL_DOES_NOT_EXIST);
        }

    }

}
