using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly string loggedUserId;
        public BaseController()
        {
            this.loggedUserId = User.Identity.GetUserId();
        }

        protected string GetLoggedUserId
        {
            get
            {
                return this.loggedUserId;
            }
        }
    }
}