using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BrumWithMe.Web.Models.Manage;
using BrumWithMe.Auth.Identity.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using System.IO;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private readonly IAccountManagementService accountManagementService;
        private readonly ICarService carService;
        private readonly IMappingProvider mappingProvider;
        private readonly IAuthService authService;

        public ManageController(
            IAuthService authService,
            ICarService carService,
            IMappingProvider mappingProvider,
            IAccountManagementService accountManagementService)
        {
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();
            Guard.WhenArgument(accountManagementService, nameof(accountManagementService)).IsNull().Throw();
            Guard.WhenArgument(carService, nameof(carService)).IsNull().Throw();
            Guard.WhenArgument(authService, nameof(authService)).IsNull().Throw();

            this.accountManagementService = accountManagementService;
            this.mappingProvider = mappingProvider;
            this.carService = carService;
            this.authService = authService;
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
                return this.View(car);
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

        public ActionResult ChangeAvatar()
        {
            var loggedUser = base.GetLoggedUserId;
            var userAvatarUrl = this.accountManagementService.GetUserAvatarUrl(loggedUser);
            var model = new ChangeAvatarViewModel();

            model.CurrentAvatarUrl = userAvatarUrl;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAvatar(ChangeAvatarViewModel changeAvatarViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(changeAvatarViewModel);
            }

            var loggedUserName = this.User.Identity.Name;

            var extension = Path.GetExtension(changeAvatarViewModel.NewAvatar.FileName);

            var filename = loggedUserName + extension;
            var path = Server.MapPath($"~/UserAvatars/{loggedUserName}/") + filename;

            changeAvatarViewModel.NewAvatar.SaveAs(path);

            var imageUrl = $"/UserAvatars/{loggedUserName}/" + filename;
            var logedUserId = base.GetLoggedUserId;

            this.accountManagementService.SetUserAvatar(logedUserId, imageUrl);

            return this.RedirectToAction(nameof(ManageController.ChangeAvatar));
        }
    }
}