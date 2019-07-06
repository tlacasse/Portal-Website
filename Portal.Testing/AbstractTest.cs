using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Testing.Fakes.Data;

namespace Portal.Testing {

    [TestClass]
    public abstract class AbstractTest {

        protected FakeWebsiteState FakeWebsiteState = new FakeWebsiteState();

        [ClassInitialize()]
        public void ClassInitialize(TestContext testContext) {
            Setup();
        }

        [ClassCleanup()]
        public void ClassCleanup() {
            CleanUp();
        }

        [TestInitialize()]
        public void TestInitialize() {
            BeforeTest();
        }

        [TestCleanup()]
        public void TestCleanup() {
            AfterTest();
        }

        public virtual void Setup() {
        }

        public virtual void CleanUp() {
        }

        public virtual void BeforeTest() {
        }

        public virtual void AfterTest() {
        }

    }

}
