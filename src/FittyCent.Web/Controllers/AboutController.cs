using System.Web.Mvc;

namespace FittyCent.Web.Controllers {
    public class AboutController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult PersonalTrainers() {
            return View();
        }

        public ActionResult Participants() {
            return View();
        }

        public ActionResult Councils() {
            return View();
        }
    }
}