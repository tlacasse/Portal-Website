using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Data.Models;
using Portal.Tests.Fakes;

namespace Portal.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconIsAnExistingIcon : TestBase {

        private Icon existingIcon;
        private Icon icon;
        private Icon newIcon;
        private IconHistory history;

        [TestInitialize]
        public void Setup() {
            FakeDatabase database = new FakeDatabase();
            IconUploadRequest request = new IconUploadRequest(WebsiteState,
                new FakeDatabaseFactory(database), new FakeEmptyFileReceiver());

            existingIcon = new Icon() {
                Id = 5,
                Name = "Test Icon",
                Image = "png",
                Link = "a.com/b"
            };

            database.WorkingDatabase[typeof(Icon)] = new List<IModel>() {
                existingIcon
            };

            icon = new Icon() {
                Id = 5,
                Name = "Test Icon",
                Image = "png",
                Link = "a.com"
            };

            request.Process(icon);

            newIcon = database.UpdatedObjects[typeof(Icon)].Single() as Icon;
            history = database.InsertedObjects[typeof(IconHistory)].Single() as IconHistory;
        }

        [TestMethod]
        public void IconPropertiesWereSaved() {
            Assert.AreEqual(icon.Name, newIcon.Name);
            Assert.AreEqual(icon.Image, newIcon.Image);
            Assert.AreEqual(icon.Link, newIcon.Link);
            Assert.IsFalse(newIcon.IsNew);

            Assert.AreEqual(existingIcon.Name, newIcon.Name);
            Assert.AreEqual(existingIcon.Image, newIcon.Image);
            Assert.AreNotEqual(existingIcon.Link, newIcon.Link);

            Assert.IsNotNull(icon.DateChanged);
        }

        [TestMethod]
        public void HistoryIsAccurate() {
            Assert.AreEqual(newIcon.Name, history.Name);
            Assert.AreEqual(newIcon.Image, history.Image);
            Assert.AreEqual(newIcon.Link, history.Link);
            Assert.AreNotEqual(existingIcon.Link, history.Link);
            Assert.IsNotNull(history.DateUpdated);
            Assert.IsFalse(history.IsNew);
        }

    }

}
