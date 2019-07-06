using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Data.Web.Form;
using Portal.Models.Portal;
using Portal.Requests.Portal;
using Portal.Requests.Portal.Results;
using Portal.Testing.Fakes.Data;

namespace Portal.Testing.Portal.TheIconUploadRequest {

    [TestClass]
    public class TestIconUploadSuccessOnNewIcon : AbstractTest {

        private IconUploadRequestResult Result;
        private Icon SubmittedIcon;

        public override void Setup() {
            SubmittedIcon = PortalTestUtility.GetIcon();
            SubmittedIcon.Id = -1;
            FakeFileReceiver fakeFileReceiver = new FakeFileReceiver(100);
            IconUploadRequest request = new IconUploadRequest(
                new FakeConnectionFactory<Icon>(),
                FakeWebsiteState,
                fakeFileReceiver
                );

            Result = request.Process(SubmittedIcon);
        }

        private static int QueryCounter { get; set; } = 0;

        private IList<Icon> 

        [TestMethod]
        public void IconUploadSuccessOnNewIcon_IfNewIcon() {
            Assert.IsTrue(result.SubmittedIcon.IsNew);
        }

        [TestMethod]
        public void IconUploadSuccessOnNewIcon_SavedIconIsCorrect() {
            Assert.AreEqual();
        }

        [TestMethod]
        public void IconUploadSuccessOnNewIcon_FileSaved() {
            Assert.IsTrue(FakeFileReceiver.ContainsSavedFile(result.PostedFile));
        }

    }

}
