using System.Web.Mvc;
using BrumWithMe.Auth.Identity.Contracts;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseAuthController : BaseController
    {
        private readonly IAuthService authService;

        public BaseAuthController(IAuthService authService)
        {
            Guard.WhenArgument(authService, nameof(authService)).IsNull().Throw();

            this.authService = authService;
        }

        protected IAuthService AuthService => this.authService;

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}