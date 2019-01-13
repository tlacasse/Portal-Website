using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal;
using Portal.Data;
using Portal.Models.Portal;
using Portal.Testing.Portal.Fakes;
using Portal.Website.Data;
using Portal.Website.Data.Logic.Portal;

namespace Portal.Testing.Portal {

    // not very well designed tests.
    [TestClass]
    public class TestIconUpload {

        private static IConnection Connection;
        private static IPostedFile SmallFile;
        private static IPostedFile BigFile;

        private Icon Icon { get; set; }
        private IFormPost Form { get; set; }

        [ClassInitialize]
        public static void Init(TestContext context) {
            Connection = new FakeIconConnection();
            SmallFile = new FakeIconPostedFile(10);
            BigFile = new FakeIconPostedFile(999_999_999);
        }

        private static Icon GetGoodIcon() {
            return new Icon() {
                Name = "Hello",
                Link = "website.com",
                Id = -1,
                Image = "png"
            };
        }

        [TestMethod]
        public void IconValidation_Nothing() {
            Icon = new Icon();
            Assert.ThrowsException<ArgumentNullException>(() => Icon.ValidateData());
        }

        [TestMethod]
        public void IconValidation_MissingValues() {
            Icon = GetGoodIcon();
            Icon.Name = null;
            Assert.ThrowsException<ArgumentNullException>(() => Icon.ValidateData());

            Icon = GetGoodIcon();
            Icon.Link = null;
            Assert.ThrowsException<ArgumentNullException>(() => Icon.ValidateData());

            Icon = GetGoodIcon();
            Icon.Image = null;
            Assert.ThrowsException<ArgumentNullException>(() => Icon.ValidateData());
        }

        [TestMethod]
        public void IconValidation_TooLongValues() {
            Icon = GetGoodIcon();
            Icon.Name = new string('q', 1234);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Icon.ValidateData());

            Icon = GetGoodIcon();
            Icon.Link = new string('q', 1234);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Icon.ValidateData());
        }

        [TestMethod]
        public void IconValidation_InvalidChars() {
            Icon = GetGoodIcon();
            Icon.Name = "qqqq85983 %";
            Assert.ThrowsException<ArgumentException>(() => Icon.ValidateData());
            Icon.Name = ".5754";
            Assert.ThrowsException<ArgumentException>(() => Icon.ValidateData());
            Icon.Name = "~";
            Assert.ThrowsException<ArgumentException>(() => Icon.ValidateData());
            Icon.Name = "\n hdhteaht";
            Assert.ThrowsException<ArgumentException>(() => Icon.ValidateData());
            Icon.Name = "*(* gsafgsf";
            Assert.ThrowsException<ArgumentException>(() => Icon.ValidateData());
            Icon.Name = "aaaaaaaaaaaaaaaaa \t";
            Assert.ThrowsException<ArgumentException>(() => Icon.ValidateData());
        }

        [TestMethod]
        public void IconUpload_GetIcon() {
            Form = new FakeIconFormPost("One", "two.com", null, null);
            Icon = Form.GetIcon();
            Assert.AreEqual("One", Icon.Name);
            Assert.AreEqual("two.com", Icon.Link);
            Assert.AreEqual(-1, Icon.Id);
            Assert.IsNull(Icon.Image);

            Form = new FakeIconFormPost("Three", "four.com", 5, SmallFile);
            Icon = Form.GetIcon();
            Assert.AreEqual("Three", Icon.Name);
            Assert.AreEqual("four.com", Icon.Link);
            Assert.AreEqual(5, Icon.Id);
            Assert.AreEqual("png", Icon.Image);
        }

        [TestMethod]
        public void IconUpload_NewIcon() {
            // name already exists
            Form = new FakeIconFormPost("Existing Icon", "test.com", null, SmallFile);
            Assert.ThrowsException<PortalException>(() => UploadIcon());
        }

        [TestMethod]
        public void IconUpload_ExistingIcon() {
            // file too big
            Form = new FakeIconFormPost("Existing Icon", "test.com", 1, BigFile);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => UploadIcon());

            // name already exists, different id
            Form = new FakeIconFormPost("Existing Icon", "test.com", 99, null);
            Assert.ThrowsException<PortalException>(() => UploadIcon());

            // id does not exist
            Form = new FakeIconFormPost("Updated Icon", "test.com", 99, null);
            Assert.ThrowsException<PortalException>(() => UploadIcon());
        }

        [TestMethod]
        public void IconUpload_CheckCleanNewIcon() {
            Form = new FakeIconFormPost("New Icon", "unique.com", null, SmallFile);
            Assert.ThrowsException<PortalException>(() => UploadIcon());
        }

        [TestMethod]
        public void IconUpload_CheckExistingIcon() {
            Form = new FakeIconFormPost("Existing Icon", "new link!", 1, SmallFile);
            Assert.ThrowsException<PortalException>(() => UploadIcon());
        }

        private void UploadIcon() {
            Form.UploadIcon(() => Connection, "");
        }

    }

}
