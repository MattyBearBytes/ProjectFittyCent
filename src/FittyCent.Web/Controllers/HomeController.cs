﻿using System.Web.Mvc;

namespace FittyCent.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }
    }
}