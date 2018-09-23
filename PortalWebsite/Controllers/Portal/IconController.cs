using Portal;
using Portal.Data;
using Portal.Models.Portal;
using PortalWebsite.Data;
using PortalWebsite.Data.Logic.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;

namespace PortalWebsite.Portal.Controllers {

    /// <summary>
    /// Anything related to Icons.
    /// </summary>
    [RoutePrefix("api/portal/icon")]
    public class IconController : ApiController {

        [HttpGet]
        [Route("list")]
        public IList<Icon> GetIconList() {
            using (Connection connection = new Connection()) {
                return connection.GetIconList();
            }
        }

        [HttpGet]
        [Route("get/{urlName}")]
        public Icon GetIconByName(string urlName) {
            string name = PortalUtility.UnUrlFormat(urlName);
            Icon icon;
            using (Connection connection = new Connection()) {
                icon = connection.GetIconByName(name);
            }
            if (icon == null) {
                throw new PortalException(string.Format("Icon '{0}' not found", name));
            }
            return icon;
        }

        [HttpPost]
        [Route("post")]
        public async Task<HttpResponseMessage> UpdateIconAsync() {
            FormPost form = await FormPost.LoadDataAsync(Request);
            form.UploadIcon(() => new Connection());
            return Request.CreateResponse(HttpStatusCode.Created);
        }

    }

}
