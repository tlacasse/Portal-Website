using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconImageFileIsTooBig : TestBase {

        private IconUploadRequest request;
        private IconPost post;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            request = new IconUploadRequest(cf,
                IconService, WebsiteState,
                new FakeFileReceiver(10000000, "Test File"));

            post = new IconPost() {
                Id = -1,
                Name = "Test Icon",
                Link = "a.com/b"
            };
        }

        [TestMethod]
        public void FailOnTooLargeFile() {
            ExpectPortalException(() => request.Process(post), IconResult.FAIL_FILE_TOO_LARGE);
        }

    }

}
