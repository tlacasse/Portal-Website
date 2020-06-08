using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Data.Models.Portal;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsExistingAndUpdated : TestBase {

        private static readonly int EXISTING_ID = 5;
        private static readonly string EXISTING_NAME = "Test Icon";
        private static readonly string EXISTING_IMAGE = "png";
        private static readonly string EXISTING_LINK = "a.com/b";
        private static readonly string NEW_LINK = "b.org/c";

        private Icon newIcon;
        private IconHistory history;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            IconUploadRequest request = new IconUploadRequest(cf,
                IconService, WebsiteState, new FakeEmptyFileReceiver());

            cf.IconsList.Init(new Icon() {
                Id = EXISTING_ID,
                Name = EXISTING_NAME,
                Image = EXISTING_IMAGE,
                Link = EXISTING_LINK
            });

            IconPost post = new IconPost() {
                Id = EXISTING_ID,
                Name = EXISTING_NAME,
                Link = NEW_LINK
            };

            request.Process(post);

            newIcon = cf.IconsList.Records.Single();
            history = cf.IconHistoriesList.Records.Single();
        }

        [TestMethod]
        public void IconPropertiesWereSaved() {
            Assert.AreEqual(EXISTING_NAME, newIcon.Name);
            Assert.AreEqual(EXISTING_IMAGE, newIcon.Image);
            Assert.AreEqual(NEW_LINK, newIcon.Link);
            Assert.IsFalse(newIcon.IsNew);
            Assert.IsNotNull(newIcon.DateChanged);
        }

        [TestMethod]
        public void HistoryIsAccurate() {
            Assert.AreEqual(EXISTING_NAME, history.Name);
            Assert.AreEqual(EXISTING_IMAGE, history.Image);
            Assert.AreEqual(NEW_LINK, history.Link);
            Assert.AreNotEqual(EXISTING_LINK, history.Link);
            Assert.IsNotNull(history.DateUpdated);
            Assert.IsFalse(history.IsNew);
        }

    }

}
