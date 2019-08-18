using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Portal.Website.Structure {

    public class ControllerFactory : IHttpControllerSelector {

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping() {
            throw new NotImplementedException();
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request) {
            throw new NotImplementedException();
        }

    }

}