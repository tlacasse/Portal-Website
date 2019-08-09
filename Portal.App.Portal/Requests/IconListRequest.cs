using Portal.App.Portal.Models;
using Portal.App.Portal.Tables;
using Portal.Data.ActiveRecord.Storage;
using Portal.Data.Querying;
using Portal.Requests;
using Portal.Structure;
using System.Collections.Generic;

namespace Portal.App.Portal.Requests {

    public class IconListRequest : DependentBase, IRequestOut<IEnumerable<Icon>>, IService<IconListRequest> {

        private IIconTable IconTable { get; }

        public IconListRequest(IActiveContext ActiveContext, IIconTable IconTable) : base(ActiveContext) {
            this.IconTable = IconTable;
        }

        public IEnumerable<Icon> Process() {
            using (ActiveContext.Start()) {
                return IconTable.Query(new All());
            }
        }

    }

}
