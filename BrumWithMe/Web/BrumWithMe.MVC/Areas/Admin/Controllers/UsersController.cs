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
        private readonly IMappingProvider mappingProvider;

        public UsersController(IAccountManagementService accountManagementService, IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(accountManagementService, nameof(accountManagementService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.accountManagementService = accountManagementService;
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
    }
}