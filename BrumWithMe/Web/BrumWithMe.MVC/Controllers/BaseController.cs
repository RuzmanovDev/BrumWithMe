using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseController : Controller
    {
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