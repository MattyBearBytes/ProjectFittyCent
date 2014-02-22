using System.Web.Mvc;

namespace FittyCent.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult PersonalTrainers()
        {
            return View();
        }

        public ActionResult Participants()
        {
            return View();
        }

        public ActionResult Councils()
        {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }
    }
}