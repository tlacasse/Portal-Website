using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Models;
using Portal.App.Portal.Requests;
using Portal.Tests.Fakes;
using System.Collections.Generic;

namespace Portal.Tests.Portal.Requests.TestIconUploadRequest {

    [TestClass]
    public class WhenIconHasDuplicateName : TestBase {

        private IconUploadRequest request;
        private Icon icon;

        [TestInitialize]
        public void Setup() {
            FakeDatabase database = new FakeDatabase();
            request = new IconUploadRequest(WebsiteState,
               new FakeDatabaseFactory(database), new FakeFileReceiver(1, "test"));

            Icon existingIcon = new Icon() {
                Id = 5,
                Name = "Test Icon",
                Image = "png",
                Link = "a.com/b"
            };

            database.WorkingDatabase[typeof(Icon)] = new List<IModel>() {
                existingIcon
            };

            icon = new Icon() {
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
