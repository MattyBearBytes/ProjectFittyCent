using System.Web.Mvc;

namespace FittyCent.Web.Controllers 
{
    public class RegisterController : Controller 
    {
        public ActionResult Index()
        {
            return RedirectToAction("Register", "Account");
        }
    }
}