using Portal.App.Portal.Models;
using Portal.App.Portal.Tables;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using Portal.Structure;
using Portal.Tests.Fakes.Internal;
using System.Collections.Generic;

namespace Portal.Tests.Fakes.Portal {

    public class FakeIconHistoryTable : IconHistoryTable, IService<IIconHistoryTable> {

        public FakeTableBackend<IconHistory> Backend { get; } = new FakeTableBackend<IconHistory>();

        public FakeIconHistoryTable() : base(null) {
        }

        public override void Insert(IconHistory item) {
            Backend.Insert(item);
        }

        public override IEnumerable<IconHistory> Query(IWhere where, QueryOptions queryOptions = QueryOptions.None) {
            return Backend.Query(where, queryOptions);
        }

        public override int Commit() {
            return Backend.Commit();
        }

    }

}
