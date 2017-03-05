using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index(int id = 0)
        {
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoadComments(int page = 0)
        {

            return PartialView("AjaxTest");
        }

        [HttpPost]
        public ActionResult PostComment()
        {
            throw new NotImplementedException();
        }

        public ActionResult Profiles(string username = "aad")
        {
            return Content(username);
        }

        public ActionResult TestHomePage()
        {
            return View();
        }
    }
}