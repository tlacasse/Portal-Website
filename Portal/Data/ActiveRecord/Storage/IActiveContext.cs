using Portal.Data.Sqlite;
using System;

namespace Portal.Data.ActiveRecord.Storage {

    public interface IActiveContext : IDisposable {

        IConnection Start();

    }

}
