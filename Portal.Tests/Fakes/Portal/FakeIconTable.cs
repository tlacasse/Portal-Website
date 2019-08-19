using System.Collections.Generic;
using Portal.App.Portal.Models;
using Portal.App.Portal.Tables;
using Portal.Data.Querying;
using Portal.Data.Sqlite;
using Portal.Structure;
using Portal.Tests.Fakes.Internal;

namespace Portal.Tests.Fakes.Portal {

    public class FakeIconTable : IconTable, IService<IIconTable> {

        public FakeTableBackend<Icon> Backend { get; } = new FakeTableBackend<Icon>();

        public FakeIconTable() : base(null) {
        }

        public override void Insert(Icon item) {
            Backend.Insert(item);
        }

        public override void Update(Icon item) {
            Backend.Update(item);
        }

        public override IEnumerable<Icon> Query(IWhere where, QueryOptions queryOptions = QueryOptions.None) {
            return Backend.Query(where, queryOptions);
        }

        public override int Commit() {
            return Backend.Commit();
        }

    }

}
