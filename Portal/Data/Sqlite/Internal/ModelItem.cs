using Portal.Data.ActiveRecord;
using System.Collections.Generic;

namespace Portal.Data.Sqlite.Internal {

    internal class ModelItem {

        internal object Object { get; set; }

        internal Dictionary<ColumnItem, int> References { get; } = new Dictionary<ColumnItem, int>();

    }

}
