using Portal.App.Portal.Models;
using Portal.Data.Querying;
using Portal.Data.Storage;
using Portal.Data.Web;
using Portal.Requests;
using System.Collections.Generic;

namespace Portal.App.Portal.Requests {

    public class IconListRequest : DependentBase, IRequest<Void, IReadOnlyList<Icon>> {

        public IconListRequest(IWebsiteState WebsiteState, IDatabaseFactory DatabaseFactory)
            : base(WebsiteState, DatabaseFactory) {
        }

        public IReadOnlyList<Icon> Process(Void model) {
            using (IDatabase database = DatabaseFactory.Create()) {
                return database.Query<Icon>(new All());
            }
        }

    }

}
