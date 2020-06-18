using System;
using System.Collections.Generic;
using Portal.App.Banking.Messages;
using Portal.Data;
using Portal.Data.Models.Banking;

namespace Portal.App.Banking.Services {

    public class ListService : IListService {

        public ListInformation GetListInformation(Type type, IConnection connection = null) {
            return GetListInformation(type.Name, connection);
        }

        public ListInformation GetListInformation(string tableName, IConnection connection = null) {
            tableName = tableName.ToLower();
            if (tableName == "account") {
                return new ListInformation() {
                    ListColumns = new List<ListColumn> {
                        new ListColumn("Id"),
                        new ListColumn("Name"),
                        new ListColumn("AccountType", typeof(AccountType))
                    },
                    ListQuery = null,//connection?.AccountQuery,
                    Type = typeof(Account),
                    AddToTable = null,//(obj) => connection.AccountTable.Add((Account)obj),
                    SelectById = (id) => connection.AccountById(id),
                    SelectByName = (name) => connection.AccountByName(name)
                };
            }
            if (tableName == "accounttype") {
                return new ListInformation() {
                    ListColumns = new List<ListColumn> {
                        new ListColumn("Id"),
                        new ListColumn("Name"),
                    },
                    ListQuery = null,//connection?.AccountTypeQuery,
                    Type = typeof(AccountType),
                    AddToTable = null,//(obj) => connection.AccountTypeTable.Add((AccountType)obj),
                    SelectById = (id) => connection.AccountTypeById(id),
                    SelectByName = (name) => connection.AccountTypeByName(name)
                };
            }
            if (tableName == "category") {
                return new ListInformation() {
                    ListColumns = new List<ListColumn> {
                        new ListColumn("Id"),
                        new ListColumn("Name"),
                    },
                    ListQuery = null,//connection?.CategoryQuery,
                    Type = typeof(Category),
                    AddToTable = null,//(obj) => connection.CategoryTable.Add((Category)obj),
                    SelectById = (id) => connection.CategoryById(id),
                    SelectByName = (name) => connection.CategoryByName(name)
                };
            }
            if (tableName == "subcategory") {
                return new ListInformation() {
                    ListColumns = new List<ListColumn> {
                        new ListColumn("Id"),
                        new ListColumn("Name"),
                        new ListColumn("Category", typeof(Category))
                    },
                    ListQuery = null,// connection?.SubcategoryQuery,
                    Type = typeof(Subcategory),
                    AddToTable = null,//(obj) => connection.SubcategoryTable.Add((Subcategory)obj),
                    SelectById = (id) => connection.SubcategoryById(id),
                    SelectByName = (name) => connection.SubcategoryByName(name)
                };
            }
            throw new PortalException(string.Format("List table not available: {0}", tableName));
        }

    }

}
