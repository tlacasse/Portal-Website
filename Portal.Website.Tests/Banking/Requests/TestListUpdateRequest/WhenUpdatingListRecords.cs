using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.App.Banking.Messages;
using Portal.App.Banking.Requests;
using Portal.Data.Models.Banking;
using Portal.Website.Tests.Fakes;

namespace Portal.Website.Tests.Banking.Requests.TestListUpdateRequest {

    [TestClass]
    public class WhenUpdatingListRecords : TestBase {

        private static readonly string ACCOUNT_TYPE = "AccountType";
        private static readonly string ACCOUNT = "Account";
        private static readonly string CATEGORY = "Category";
        private static readonly string SUBCATEGORY = "SubCategory";

        private static readonly string ACCOUNT_NAME = "test account name";
        private static readonly string SUBCATEGORY_NAME = "test subcatgeory name";
        private static readonly string ACCOUNT_TYPE_NAME_TOUPDATE = "account type to be updated";
        private static readonly string CATEGORY_NAME_TOUPDATE = "category to be updated";

        private static readonly string ACCOUNT_TYPE_NAME_TOUSE = "account type to switch to";
        private static readonly string CATEGORY_NAME_TOUSE = "category to switch to";

        private static readonly string ACCOUNT_TYPE_NAME_NEW = "new account type";
        private static readonly string CATEGORY_NAME_NEW = "new category";

        private static readonly int CHANGING_ACCOUNT_ID = 10;
        private static readonly int CHANGING_ACCOUNT_TYPE_ID = 15;
        private static readonly int CHANGING_CATEGORY_ID = 20;
        private static readonly int CHANGING_SUBCATEGORY_ID = 25;
        private static readonly int CHANGETO_ACCOUNT_TYPE_ID = 30;
        private static readonly int CHANGETO_CATEGORY_ID = 35;

        private Account account;
        private Subcategory subcategory;
        private Category category;
        private AccountType accountType;
        private FakeConnectionFactory cf;

        private IEnumerable<IEnumerable<string>> accountResult;
        private IEnumerable<IEnumerable<string>> accountTypeResult;
        private IEnumerable<IEnumerable<string>> categoryResult;
        private IEnumerable<IEnumerable<string>> subcategoryResult;

        protected override void Before() {
            cf = new FakeConnectionFactory();
            ListUpdateRequest request = new ListUpdateRequest(cf, ListService);

            AccountType type1 = new AccountType() {
                Id = CHANGING_ACCOUNT_TYPE_ID,
                Name = ACCOUNT_TYPE_NAME_TOUPDATE
            };
            AccountType type2 = new AccountType() {
                Id = CHANGETO_ACCOUNT_TYPE_ID,
                Name = ACCOUNT_TYPE_NAME_TOUSE
            };
            Category cat1 = new Category() {
                Id = CHANGING_CATEGORY_ID,
                Name = CATEGORY_NAME_TOUPDATE
            };
            Category cat2 = new Category() {
                Id = CHANGETO_CATEGORY_ID,
                Name = CATEGORY_NAME_TOUSE
            };
            Account accountinit = new Account() {
                Id = CHANGING_ACCOUNT_ID,
                Name = ACCOUNT_NAME,
                AccountType = type1
            };
            Subcategory subcatinit = new Subcategory() {
                Id = CHANGING_SUBCATEGORY_ID,
                Name = SUBCATEGORY_NAME,
                Category = cat1
            };

            cf.AccountTypeInternal.Init(type1);
            cf.AccountTypeInternal.Init(type2);
            cf.AccountInternal.Init(accountinit);
            cf.CategoryInternal.Init(cat1);
            cf.CategoryInternal.Init(cat2);
            cf.SubcategoryInternal.Init(subcatinit);

            string[] updatedAccount = new string[] { ACCOUNT, CHANGING_ACCOUNT_ID.ToString(),
                ACCOUNT_NAME, ACCOUNT_TYPE_NAME_TOUSE };
            string[] updatedSubcategory = new string[] { SUBCATEGORY, CHANGING_SUBCATEGORY_ID.ToString(),
                SUBCATEGORY_NAME, CATEGORY_NAME_TOUSE };
            string[] updatedAccountType = new string[] { ACCOUNT_TYPE, CHANGING_ACCOUNT_TYPE_ID.ToString(),
                ACCOUNT_TYPE_NAME_NEW };
            string[] updatedCategory = new string[] { CATEGORY, CHANGING_CATEGORY_ID.ToString(),
                CATEGORY_NAME_NEW };

            request.Process(new ListUpdate(updatedAccount));
            request.Process(new ListUpdate(updatedSubcategory));
            request.Process(new ListUpdate(updatedAccountType));
            request.Process(new ListUpdate(updatedCategory));

            accountType = cf.AccountTypeInternal.Records.Take(1).Single();
            category = cf.CategoryInternal.Records.Take(1).Single();
            account = cf.AccountInternal.Records.Single();
            subcategory = cf.SubcategoryInternal.Records.Single();

            ListDataRequest dataRequest = new ListDataRequest(cf, ListService);
            accountResult = dataRequest.Process("Account");
            accountTypeResult = dataRequest.Process("AccountType");
            categoryResult = dataRequest.Process("Category");
            subcategoryResult = dataRequest.Process("Subcategory");
        }

        [TestMethod]
        public void RecordCountsShouldBeCorrect() {
            Assert.AreEqual(2, cf.AccountTypeInternal.Records.Count());
            Assert.AreEqual(2, cf.CategoryInternal.Records.Count());
            Assert.AreEqual(1, cf.AccountInternal.Records.Count());
            Assert.AreEqual(1, cf.SubcategoryInternal.Records.Count());
        }

        [TestMethod]
        public void AccountShouldBeUpdated() {
            Assert.AreEqual(CHANGING_ACCOUNT_ID, account.Id);
            Assert.AreEqual(ACCOUNT_NAME, account.Name);
            Assert.AreEqual(CHANGETO_ACCOUNT_TYPE_ID, account.AccountType.Id);
            Assert.AreEqual(ACCOUNT_TYPE_NAME_TOUSE, account.AccountType.Name);
            Assert.IsNotNull(accountType.DateUpdated);
        }

        [TestMethod]
        public void SubcategoryShouldBeUpdated() {
            Assert.AreEqual(CHANGING_SUBCATEGORY_ID, subcategory.Id);
            Assert.AreEqual(SUBCATEGORY_NAME, subcategory.Name);
            Assert.AreEqual(CHANGETO_CATEGORY_ID, subcategory.Category.Id);
            Assert.AreEqual(CATEGORY_NAME_TOUSE, subcategory.Category.Name);
            Assert.IsNotNull(subcategory.DateUpdated);
        }

        [TestMethod]
        public void AccountTypeShouldBeUpdated() {
            Assert.AreEqual(CHANGING_ACCOUNT_TYPE_ID, accountType.Id);
            Assert.AreEqual(ACCOUNT_TYPE_NAME_NEW, accountType.Name);
            Assert.IsNotNull(accountType.DateUpdated);
        }

        [TestMethod]
        public void CategoryShouldBeUpdated() {
            Assert.AreEqual(CHANGING_CATEGORY_ID, category.Id);
            Assert.AreEqual(CATEGORY_NAME_NEW, category.Name);
            Assert.IsNotNull(category.DateUpdated);
        }

        [TestMethod]
        public void AccountResultsShouldBeCorrect() {
            IEnumerable<string> record = accountResult.Single();
            Assert.AreEqual(ACCOUNT_NAME, record.Skip(1).Take(1).Single());
            Assert.AreEqual(ACCOUNT_TYPE_NAME_TOUSE, record.Skip(2).Single());
        }

        [TestMethod]
        public void AccountTypeResultsShouldBeCorrect() {
            IEnumerable<string> record = accountTypeResult.Take(1).Single();
            Assert.AreEqual(ACCOUNT_TYPE_NAME_NEW, record.Skip(1).Single());
            record = accountTypeResult.Skip(1).Single();
            Assert.AreEqual(ACCOUNT_TYPE_NAME_TOUSE, record.Skip(1).Single());
        }

        [TestMethod]
        public void CategoryResultsShouldBeCorrect() {
            IEnumerable<string> record = categoryResult.Take(1).Single();
            Assert.AreEqual(CATEGORY_NAME_NEW, record.Skip(1).Single());
            record = categoryResult.Skip(1).Single();
            Assert.AreEqual(CATEGORY_NAME_TOUSE, record.Skip(1).Single());
        }

        [TestMethod]
        public void SubcategoryResultsShouldBeCorrect() {
            IEnumerable<string> record = subcategoryResult.Single();
            Assert.AreEqual(SUBCATEGORY_NAME, record.Skip(1).Take(1).Single());
            Assert.AreEqual(CATEGORY_NAME_TOUSE, record.Skip(2).Single());
        }

    }

}
