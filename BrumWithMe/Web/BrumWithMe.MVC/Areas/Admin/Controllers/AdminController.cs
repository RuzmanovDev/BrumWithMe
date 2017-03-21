using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            return this.View();
        }

        public ActionResult ReportedTrips()
        {
            return this.PartialView("_ReportedTrips");
        }
    }
}