using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BrumWithMe.Web.Models.Manage;
using BrumWithMe.Auth.Identity.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.FileUpload;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using System.IO;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class ManageController : BaseAuthController
    {
        private readonly IAccountManagementService accountManagementService;
        private readonly ICarService carService;
        private readonly IMappingProvider mappingProvider;

        public ManageController(
            IAuthService authService,
            ICarService carService,
            IMappingProvider mappingProvider,
            IAccountManagementService accountManagementService)
            : base(authService)
        {
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();
            Guard.WhenArgument(accountManagementService, nameof(accountManagementService)).IsNull().Throw();
            Guard.WhenArgument(carService, nameof(carService)).IsNull().Throw();

            this.accountManagementService = accountManagementService;
            this.mappingProvider = mappingProvider;
            this.carService = carService;
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

            var imageUrl = "/UserAvatars/default-car.jpg";

            if (car.CarAvatar != null)
            {
                var loggedUserName = this.User.Identity.Name;

                var extension = Path.GetExtension(car.CarAvatar.FileName);

                var filename = loggedUserName + car.Model + car.Model + car.Year + extension;
                var path = Server.MapPath($"~/UserAvatars/{loggedUserName}/Cars/") + filename;

                car.CarAvatar.SaveAs(path);

                imageUrl = $"/UserAvatars/{loggedUserName}/Cars/" + filename;
            }

            var carToAdd = this.mappingProvider.Map<RegisterCarViewModel, Car>(car);
            carToAdd.ImageUrl = imageUrl;

            var loggedUser = base.GetLoggedUserId;
            this.carService.AddCarToUser(carToAdd, loggedUser);

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

            IdentityResult result = await this.AuthService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                this.AuthService.LogOff();

                return RedirectToAction("Index");
            }

            AddErrors(result);

            return View(model);
        }
    }
}