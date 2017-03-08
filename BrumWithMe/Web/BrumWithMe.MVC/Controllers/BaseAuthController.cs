using System.Web.Mvc;
using BrumWithMe.Auth.Identity.Contracts;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseAuthController : Controller
    {
        private readonly string loggedUserId;
        private readonly IAuthService authService;

        public BaseAuthController(IAuthService authService)
        {
            Guard.WhenArgument(authService, nameof(authService)).IsNull().Throw();

            this.authService = authService;
            this.loggedUserId = this.authService.GetLoggedUserId(this.User);
        }

        protected string GetLoggedUserId => this.loggedUserId;

        protected IAuthService AuthService => this.authService;

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}