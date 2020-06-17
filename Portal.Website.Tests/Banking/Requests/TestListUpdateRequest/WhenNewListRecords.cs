using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Banking.Messages;
using Portal.App.Banking.Requests;
using Portal.Data.Models.Banking;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Banking.Requests.TestListUpdateRequest {

    [TestClass]
    public class WhenNewListRecords : TestBase {

        private static readonly string ACCOUNT_TYPE = "AccountType";
        private static readonly string ACCOUNT = "Account";
        private static readonly string CATEGORY = "Category";
        private static readonly string SUBCATEGORY = "SubCategory";
        private static readonly string NEW_ID = "-1";

        private static readonly string ACCOUNT_TYPE_NAME = "savings";
        private static readonly string ACCOUNT_NAME = "good bank";
        private static readonly string CATEGORY_NAME = "food";
        private static readonly string SUBCATEGORY_NAME = "ice cream";

        private AccountType accountType;
        private Account account;
        private Category category;
        private Subcategory subcategory;

        private IEnumerable<IEnumerable<string>> accountResult;
        private IEnumerable<IEnumerable<string>> accountTypeResult;
        private IEnumerable<IEnumerable<string>> categoryResult;
        private IEnumerable<IEnumerable<string>> subcategoryResult;

        protected override void Before() {
            FakeConnectionFactory cf = new FakeConnectionFactory();
            ListUpdateRequest request = new ListUpdateRequest(cf, ListService);

            string[] newAccountType = new string[] { ACCOUNT_TYPE, NEW_ID, ACCOUNT_TYPE_NAME };
            string[] newAccount = new string[] { ACCOUNT, NEW_ID, ACCOUNT_NAME, ACCOUNT_TYPE_NAME };
            string[] newCategory = new string[] { CATEGORY, NEW_ID, CATEGORY_NAME };
            string[] newSubcategory = new string[] { SUBCATEGORY, NEW_ID, SUBCATEGORY_NAME, CATEGORY_NAME };

            request.Process(new ListUpdate(newAccountType));
            request.Process(new ListUpdate(newAccount));
            request.Process(new ListUpdate(newCategory));
            request.Process(new ListUpdate(newSubcategory));

            accountType = cf.AccountTypeInternal.Records.Single();
            account = cf.AccountInternal.Records.Single();
            category = cf.CategoryInternal.Records.Single();
            subcategory = cf.SubcategoryInternal.Records.Single();

            ListDataRequest dataRequest = new ListDataRequest(cf, ListService);
            accountResult = dataRequest.Process("Account");
            accountTypeResult = dataRequest.Process("AccountType");
            categoryResult = dataRequest.Process("Category");
            subcategoryResult = dataRequest.Process("Subcategory");
        }

        [TestMethod]
        public void AccountTypeShouldBeSaved() {
            Assert.AreEqual(ACCOUNT_TYPE_NAME, accountType.Name);
            Assert.IsNotNull(accountType.DateUpdated);
        }

        [TestMethod]
        public void AccountShouldBeSaved() {
            Assert.AreEqual(ACCOUNT_NAME, account.Name);
            Assert.AreEqual(ACCOUNT_TYPE_NAME, account.AccountType.Name);
            Assert.IsNotNull(account.DateUpdated);
        }

        [TestMethod]
        public void CategoryShouldBeSaved() {
            Assert.AreEqual(CATEGORY_NAME, category.Name);
            Assert.IsNotNull(category.DateUpdated);
        }

        [TestMethod]
        public void SubcategoryShouldBeSaved() {
            Assert.AreEqual(SUBCATEGORY_NAME, subcategory.Name);
            Assert.AreEqual(CATEGORY_NAME, subcategory.Category.Name);
            Assert.IsNotNull(subcategory.DateUpdated);
        }

        [TestMethod]
        public void AccountResultsShouldBeCorrect() {
            IEnumerable<string> record = accountResult.Single();
            Assert.AreEqual(ACCOUNT_NAME, record.Skip(1).Take(1).Single());
            Assert.AreEqual(ACCOUNT_TYPE_NAME, record.Skip(2).Single());
        }

        [TestMethod]
        public void AccountTypeResultsShouldBeCorrect() {
            IEnumerable<string> record = accountTypeResult.Single();
            Assert.AreEqual(ACCOUNT_TYPE_NAME, record.Skip(1).Single());
        }

        [TestMethod]
        public void CategoryResultsShouldBeCorrect() {
            IEnumerable<string> record = categoryResult.Single();
            Assert.AreEqual(CATEGORY_NAME, record.Skip(1).Single());
        }

        [TestMethod]
        public void SubcategoryResultsShouldBeCorrect() {
            IEnumerable<string> record = subcategoryResult.Single();
            Assert.AreEqual(SUBCATEGORY_NAME, record.Skip(1).Take(1).Single());
            Assert.AreEqual(CATEGORY_NAME, record.Skip(2).Single());
        }

    }

}
