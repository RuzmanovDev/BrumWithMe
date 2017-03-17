using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            return View();
        }
    }
}