using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseController : Controller
    {
        public Func<string> GetUserId;

        public BaseController()
        {
            this.GetUserId = () => User.Identity.GetUserId();
        }

        protected virtual string GetLoggedUserId
        {
            get
            {
                return this.User?.Identity.GetUserId();
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}