using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Messages;
using Portal.App.Portal.Requests;
using Portal.Data.Models.Portal;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsANewIcon : TestBase {

        private static readonly string LINK = "a.com/b";

        private Icon newIcon;
        private IconHistory history;
        private FakeFileReceiver fileReceiver;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            fileReceiver = new FakeFileReceiver(1, "Test File");
            IconUploadRequest request = new IconUploadRequest(cf,
                IconService, WebsiteState, fileReceiver);

            IconPost post = new IconPost() {
                Id = -1,
                Name = "test icon",
                Link = LINK
            };

            request.Process(post);

            newIcon = cf.IconsList.Records.Single();
            history = cf.IconHistoriesList.Records.Single();
        }

        [TestMethod]
        public void NameIsFormattedCorrectly() {
            Assert.AreEqual("Test Icon", newIcon.Name);
        }

        [TestMethod]
        public void ImageAndLinkWereSaved() {
            Assert.AreEqual(FakeFileReceiver.EXTENSION, newIcon.Image);
            Assert.AreEqual(LINK, newIcon.Link);
            Assert.IsTrue(newIcon.IsNew);
        }

        [TestMethod]
        public void SavedIconHasDates() {
            Assert.IsNotNull(newIcon.DateCreated);
            Assert.IsNotNull(newIcon.DateChanged);
            Assert.AreNotEqual(default(DateTime), newIcon.DateCreated);
            Assert.AreNotEqual(default(DateTime), newIcon.DateChanged);
        }

        [TestMethod]
        public void HistoryIsAccurate() {
            Assert.AreEqual(newIcon.Name, history.Name);
            Assert.AreEqual(newIcon.Image, history.Image);
            Assert.AreEqual(newIcon.Link, history.Link);
            Assert.IsTrue(history.IsNew);
            Assert.IsNotNull(history.DateUpdated);
            Assert.AreNotEqual(default(DateTime), history.DateUpdated);
            Assert.IsTrue(history.DateUpdated >= newIcon.DateChanged);
        }

        [TestMethod]
        public void FileWasSaved() {
            Assert.AreEqual(string.Format("{0}/{1}", WebsiteState.WebsitePath,
                    string.Format(IconUploadRequest.SAVE_PATH_TEMPLATE, newIcon.Id, newIcon.Image)),
                fileReceiver.SavedFiles.Single());
        }

    }

}
