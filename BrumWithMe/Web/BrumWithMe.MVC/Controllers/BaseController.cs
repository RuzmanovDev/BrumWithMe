using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.GetLoggedUserId = () => User.Identity.GetUserId();
        }

        public virtual Func<string> GetLoggedUserId { get; set; }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}