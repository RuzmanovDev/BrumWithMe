using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BrumWithMe.Web.Models.Account;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Auth.Identity.Contracts;
using System.Collections;
using BrumWithMe.Services.Providers.FileUpload;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using System.IO;
using Bytes2you.Validation;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private const string XsrfKey = "XsrfId";
        private readonly IMappingProvider mappingProvider;
        private readonly IAuthService authService;

        public AccountController(IAuthService authService, IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(authService, nameof(authService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.mappingProvider = mappingProvider;
            this.authService = authService;
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

            var result = await this.authService.LogIn(model.Email, model.Password);

            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToAction(nameof(HomeController.Index), "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Грешно потребителско име или парола!");
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
                    AvataImageurl = "/UserAvatars/default.png"
                };

                IdentityResult result = await this.authService.Register(user, model.Password);

                if (result.Succeeded)
                {
                    await this.authService.LogIn(model.Email, model.Password);

                    var path = Server.MapPath($"~/UserAvatars/{model.Email}/Cars/");
                    Directory.CreateDirectory(path);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                
                this.AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.authService.LogOff();
            return RedirectToAction("Index", "Home");
        }
    }
}