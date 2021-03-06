﻿using Portal.Data;
using Portal.Data.Models.Portal;
using Portal.Structure;
using Portal.Structure.Requests;

namespace Portal.App.Portal.Requests {

    public class IconByNameRequest : CommonDependent, IRequest<string, Icon> {

        public IconByNameRequest(IConnectionFactory ConnectionFactory) : base(ConnectionFactory) {
        }

        public Icon Process(string model) {
            this.NeedNotNull(model, "icon name");
            string name = PortalUtility.UnUrlFormat(model);
            Icon icon;
            using (IConnection connection = ConnectionFactory.Create()) {
                icon = connection.IconByName(name);
            }
            if (icon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found", name));
            }
            return icon;
        }

    }

}
