using BrumWithMe.Auth.Identity.Contracts;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Shared;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountManagementService accountManagementService;
        private readonly IAuthService authService;
        private readonly IMappingProvider mappingProvider;

        public UsersController(
            IAccountManagementService accountManagementService,
            IMappingProvider mappingProvider,
            IAuthService authService)
        {
            Guard.WhenArgument(accountManagementService, nameof(accountManagementService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();
            Guard.WhenArgument(authService, nameof(authService)).IsNull().Throw();

            this.accountManagementService = accountManagementService;
            this.authService = authService;
            this.mappingProvider = mappingProvider;
        }

        public ActionResult Users()
        {
            return this.PartialView("_UsersTable");
        }

        public ActionResult UsersData()
        {
            var usersData = this.accountManagementService.GetAllUsersBasicInfo();
            var usersVModel = this.mappingProvider.Map<IEnumerable<UserBasicInfo>, IEnumerable<UserBannerViewModel>>(usersData);

            return this.PartialView("_UsersData", usersVModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LockoutUser(string userId, int days = 0)
        {
            this.authService.LockAccount(userId, 3);

            return this.UsersData();
        }

    }
}