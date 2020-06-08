using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Portal.Services;
using Portal.Website.Tests.Fakes;
using System;

namespace Portal.Website.Tests {

    [TestClass]
    public class TestBase {

        protected FakeWebsiteState WebsiteState { get; } = new FakeWebsiteState();
        protected IconService IconService { get; } = new IconService();

        protected virtual void Before() {
        }

        protected virtual void After() {
        }

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext) { }

        [ClassCleanup()]
        public static void ClassCleanup() { }


        [TestInitialize()]
        public void TestInitialize() {
            Before();
        }

        [TestCleanup()]
        public void TestCleanup() {
            After();
        }

        public void ExpectPortalException(Action action, object message) {
            try {
                action.Invoke();
                Assert.Fail();
            } catch (PortalException pe) {
                Assert.AreEqual(message.ToString(), pe.Message);
            }
        }

    }
}
