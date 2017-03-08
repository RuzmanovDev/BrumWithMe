using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BrumWithMe.Web.Models.Manage;
using BrumWithMe.Auth.Identity.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.FileUpload;
using System.IO;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IAuthService authService;
        private readonly IFileUploadProvider fileUploadProvider;
        private readonly IAccountManagementService accountManagementService;
        public ManageController(
            IAuthService authService,
            IFileUploadProvider fileUploadProvider,
            IAccountManagementService accountManagementService)
        {
            Guard.WhenArgument(authService, nameof(authService)).IsNull().Throw();
            Guard.WhenArgument(fileUploadProvider, nameof(fileUploadProvider)).IsNull().Throw();
            Guard.WhenArgument(accountManagementService, nameof(accountManagementService)).IsNull().Throw();

            this.authService = authService;
            this.fileUploadProvider = fileUploadProvider;
            this.accountManagementService = accountManagementService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterCar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCar(RegisterCarViewModel car)
        {
            if (!this.ModelState.IsValid)
            {
                return View(car);
            }

            var imageUrl = "";
            if (car.CarAvatar != null)
            {
                var extension = Path.GetExtension(car.CarAvatar.FileName);
                var filename = this.User.Identity.Name + car.Model + car.Year + extension;
                var path = Server.MapPath("~/UserAvatars/Cars/") + filename;

                this.fileUploadProvider.UploadCarImage(car.CarAvatar, path);

                imageUrl = "/UserAvatars/Cars/" + filename;
            }

            var carToAdd = new Car()
            {
                Color = car.Color,
                ImageUrl = imageUrl,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
            };

            //map the car from view to service
            var loggedUser = this.HttpContext.User.Identity.GetUserId();
            this.accountManagementService.AddCarToUser(carToAdd, loggedUser);

            return RedirectToAction(nameof(this.RegisterCar));
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityResult result = await this.authService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                User user = null; //await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await this.authService.LogIn(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction("Index");
            }

            AddErrors(result);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}