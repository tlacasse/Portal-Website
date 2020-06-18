using Portal.Data;
using Portal.Data.Models.Banking;
using System.Linq;

namespace Portal.App.Banking {

    public static class BankingConnectionExtensions {

        public static Account AccountById(this IConnection connection, int id) {
            return null;// connection.AccountQuery.Where(x => x.Id == id).SingleOrDefault();
        }

        public static Account AccountByName(this IConnection connection, string name) {
            return null;// connection.AccountQuery.Where(x => x.Name == name).SingleOrDefault();
        }

        public static AccountType AccountTypeById(this IConnection connection, int id) {
            return null;// connection.AccountTypeQuery.Where(x => x.Id == id).SingleOrDefault();
        }

        public static AccountType AccountTypeByName(this IConnection connection, string name) {
            return null;// connection.AccountTypeQuery.Where(x => x.Name == name).SingleOrDefault();
        }

        public static Category CategoryById(this IConnection connection, int id) {
            return null;// connection.CategoryQuery.Where(x => x.Id == id).SingleOrDefault();
        }

        public static Category CategoryByName(this IConnection connection, string name) {
            return null;// connection.CategoryQuery.Where(x => x.Name == name).SingleOrDefault();
        }

        public static Subcategory SubcategoryById(this IConnection connection, int id) {
            return null;// connection.SubcategoryQuery.Where(x => x.Id == id).SingleOrDefault();
        }

        public static Subcategory SubcategoryByName(this IConnection connection, string name) {
            return null;// connection.SubcategoryQuery.Where(x => x.Name == name).SingleOrDefault();
        }

    }

}
