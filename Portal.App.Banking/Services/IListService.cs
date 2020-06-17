using Portal.App.Banking.Messages;
using Portal.Data;
using System;

namespace Portal.App.Banking.Services {

    public interface IListService {

        ListInformation GetListInformation(Type type, IConnection connection = null);
        ListInformation GetListInformation(string tableName, IConnection connection = null);

    }

}
