using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsInvalid : TestBase {

        private IconUploadRequest request;

        protected override void Before() {
            request = new IconUploadRequest(new FakeConnectionFactory(),
                WebsiteState, IconValidatorService, new FakeEmptyFileReceiver());
        }

        [TestMethod]
        public void FailOnEmptyIcon() {
            ExpectPortalException(() => request.Process(new IconPost()), IconResult.FAIL_NO_NAME);
        }

        [TestMethod]
        public void FailOnInvalidIconName() {
            ExpectPortalException(() => request.Process(new IconPost() {
                Name = "inv@l!d ",
                Link = "a.com",
                Id = -1
            }), IconResult.FAIL_INVALID_NAME);
        }

    }

}
