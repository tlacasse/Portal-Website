using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Banking.Messages;
using Portal.App.Banking.Requests;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Banking.Requests.TestListUpdateRequest {

    [TestClass]
    public class WhenNewListRecordsButMissingSubRecord : TestBase {

        private static readonly string ACCOUNT = "Account";
        private static readonly string SUBCATEGORY = "SubCategory";
        private static readonly string NEW_ID = "-1";

        private static readonly string ACCOUNT_TYPE_NAME = "savings";
        private static readonly string ACCOUNT_NAME = "good bank";
        private static readonly string CATEGORY_NAME = "food";
        private static readonly string SUBCATEGORY_NAME = "ice cream";

        private ListUpdateRequest request;
        private string[] newAccount;
        private string[] newSubcategory;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            request = new ListUpdateRequest(cf, ListService);

            newAccount = new string[] { ACCOUNT, NEW_ID, ACCOUNT_NAME, ACCOUNT_TYPE_NAME };
            newSubcategory = new string[] { SUBCATEGORY, NEW_ID, SUBCATEGORY_NAME, CATEGORY_NAME };
        }

        [TestMethod]
        public void FailOnAccount() {
            ExpectPortalException(() => request.Process(new ListUpdate(newAccount)),
                "Missing");
        }

        [TestMethod]
        public void FailOnSubcategory() {
            ExpectPortalException(() => request.Process(new ListUpdate(newSubcategory)),
                "Missing");
        }

    }

}
