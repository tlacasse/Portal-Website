using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalWebsite.Controllers {

    public class AppController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult Portal() {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

    }

}
