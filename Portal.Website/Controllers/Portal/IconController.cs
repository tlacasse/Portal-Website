using Portal;
using Portal.Data;
using Portal.Models.Portal;
using Portal.Website.Data;
using Portal.Website.Data.Logic;
using Portal.Website.Data.Logic.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;

namespace Portal.Website.Portal.Controllers {

    [RoutePrefix("api/portal/icon")]
    public class IconController : ApiController {

        [HttpGet]
        [Route("list")]
        public IList<Icon> GetIconList() {
            return this.LogIfError(() => {
                using (Connection connection = new Connection()) {
                    return connection.GetIconList();
                }
            });
        }

        [HttpGet]
        [Route("get/{name}")]
        public Icon GetIconByName(string name) {
            return this.LogIfError(() => {
                name = PortalUtility.UnUrlFormat(name);
                Icon icon;
                using (Connection connection = new Connection()) {
                    icon = connection.GetIconByName(name);
                }
                if (icon == null) {
                    throw new PortalException(string.Format("Icon '{0}' not found", name));
                }
                return icon;
            });
        }

        [HttpPost]
        [Route("post")]
        public async Task<HttpResponseMessage> UpdateIconAsync() {
            return await this.LogIfErrorAsync(async () => {
                FormPost form = await FormPost.LoadDataAsync(Request);
                form.UploadIcon(() => new Connection());
                return Request.CreateResponse(HttpStatusCode.Created);
            });
        }

    }

}
