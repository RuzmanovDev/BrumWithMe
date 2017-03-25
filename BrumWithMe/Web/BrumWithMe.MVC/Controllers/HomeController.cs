using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAccountManagementService accountManagementService;

        public HomeController(IAccountManagementService accountManagementService)
        {
            Guard.WhenArgument(accountManagementService, nameof(accountManagementService)).IsNull().Throw();

            this.accountManagementService = accountManagementService;
        }

        public ActionResult Index(int id = 0)
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            var loggedUser = this.GetLoggedUserId();

            if (loggedUser != null)
            {
                var loggedUserAvatar = this.accountManagementService.GetUserAvatarUrl(loggedUser);
                return this.PartialView("_LoginPartial", loggedUserAvatar);
            }

            return this.PartialView("_LoginPartial");
        }
    }
}