using Portal;
using Portal.Data;
using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalWebsite.Portal.Controllers {

    [RoutePrefix("api/portal/icon")]
    public class IconController : ApiController {

        [HttpGet]
        [Route("list")]
        public IList<Icon> GetIconList() {
            return Query.GetIconList();
        }

        [HttpGet]
        [Route("get/{urlName}")]
        public Icon GetIconByName(string urlName) {
            return Query.GetIconByName(PortalUtility.UnUrlFormat(urlName));
        }
    }

}
