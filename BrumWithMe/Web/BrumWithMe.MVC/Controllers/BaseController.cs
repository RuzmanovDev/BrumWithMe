using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected string GetLoggedUserId
        {
            get
            {
                return this.User.Identity.GetUserId();
            }
        }
    }
}