using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.Requests.Portal;
using Portal.Testing.Fakes.Data;

namespace Portal.Testing.Portal.TheLastBuildTimeRequest {

    [TestClass]
    public class TestLastBuildTimeRequest : AbstractTest {

        [TestMethod]
        public void DateTimeMatches() {
            LastBuildTimeRequest request = new LastBuildTimeRequest(
                new FakeConnectionFactory<Void>(),
                FakeWebsiteState
                );
            string date = request.Process(null);
            Assert.AreEqual(FakeWebsiteState.LastGridBuildTime, DateTime.Parse(date));
        }

    }

}
