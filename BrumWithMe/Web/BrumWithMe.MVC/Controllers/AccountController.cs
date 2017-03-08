using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BrumWithMe.Web.Models.Account;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Auth.Identity.Contracts;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class AccountController : BaseAuthController
    {
        private const string XsrfKey = "XsrfId";

        public AccountController(IAuthService authService)
            :base(authService)
        {
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await this.AuthService.LogIn(model.Email, model.Password);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неуспешен опит за вход!");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    AvataImageurl = "~/UserAvatars/default.png"
                };

                IdentityResult result = await this.AuthService.Register(user, model.Password);

                if (result.Succeeded)
                {
                    await this.AuthService.LogIn(model.Email, model.Password);

                    return RedirectToAction("Index", "Home");
                }

                this.AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthService.LogOff();
            return RedirectToAction("Index", "Home");
        }


    }
}