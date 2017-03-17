using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected string GetLoggedUserId
        {
            get
            {
                return this.User?.Identity.GetUserId();
            }
        }
    }
}