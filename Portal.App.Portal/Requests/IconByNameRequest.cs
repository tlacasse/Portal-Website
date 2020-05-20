using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Structure;
using Portal.Structure.Requests;
using System.Linq;

namespace Portal.App.Portal.Requests {

    public class IconByNameRequest : CommonDependent, IRequest<string, Icon> {

        public IconByNameRequest(IConnectionFactory ConnectionFactory) : base(ConnectionFactory) {
        }

        public Icon Process(string model) {
            this.NeedNotNull(model, "icon name");
            Icon icon;
            using (IConnection connection = ConnectionFactory.Create()) {
                icon = connection.Icons.Where(x => x.Name == model).SingleOrDefault();
            }
            if (icon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found", model));
            }
            return icon;
        }

    }

}
