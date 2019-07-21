using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Tests.Fakes;
using System;
using System.Linq;

namespace Portal.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsANewIcon : TestBase {

        private Icon icon;
        private Icon newIcon;
        private IconHistory history;
        private FakeFileReceiver fileReceiver;

        [TestInitialize]
        public void Setup() {
            FakeDatabase database = new FakeDatabase();
            fileReceiver = new FakeFileReceiver(1, "test");
            IconUploadRequest request = new IconUploadRequest(WebsiteState,
                new FakeDatabaseFactory(database), fileReceiver);

            icon = new Icon() {
                Name = "test icon",
                Image = "png",
                Link = "a.com"
            };

            request.Process(icon);

            newIcon = database.InsertedObjects[typeof(Icon)].Single() as Icon;
            history = database.InsertedObjects[typeof(IconHistory)].Single() as IconHistory;
        }

        [TestMethod]
        public void NameIsFormattedCorrectly() {
            Assert.AreEqual("Test Icon", newIcon.Name);
        }

        [TestMethod]
        public void ImageAndLinkWereSaved() {
            Assert.AreEqual(icon.Image, newIcon.Image);
            Assert.AreEqual(icon.Link, newIcon.Link);
            Assert.IsTrue(newIcon.IsNew);
        }

        [TestMethod]
        public void SavedIconHasDates() {
            Assert.IsNotNull(newIcon.DateCreated);
            Assert.IsNotNull(newIcon.DateChanged);
            Assert.AreNotEqual(default(DateTime), newIcon.DateCreated);
            Assert.AreNotEqual(default(DateTime), newIcon.DateChanged);
            Assert.IsTrue(newIcon.DateCreated >= icon.DateCreated);
            Assert.IsTrue(newIcon.DateChanged >= icon.DateChanged);
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
