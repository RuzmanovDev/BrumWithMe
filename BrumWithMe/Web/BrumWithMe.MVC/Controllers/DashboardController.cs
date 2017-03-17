using System.Web.Mvc;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Dashboard;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITripService tripService;
        private readonly IMappingProvider mappingProvider;

        public DashboardController(ITripService tripService, IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(tripService, nameof(tripService)).IsNull().Throw();

            this.tripService = tripService;
            this.mappingProvider = mappingProvider;
        }

        public ActionResult Index()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var data = this.tripService.GetTripsCreatedByUser(loggedUserId);

            var model = new DashboardViewModel();
            model.TripsCreatedByUser = data;

            return View(model);
        }

        [HttpPost]
        public ActionResult AcceptUserInTrip(string userId, int tripId)
        {
            var updatedTripInfo = this.tripService.JoinUserToTrip(userId, tripId);

            return this.PartialView("_TripsSharedByCurrentUser", updatedTripInfo);
        }

        [HttpPost]
        public ActionResult RejectUserInTrip(string userId, int tripId)
        {
            var updatedTripInfo = this.tripService.RejectUserToJoinTrip(userId, tripId);

            return this.PartialView("_TripsSharedByCurrentUser", updatedTripInfo);
        }
    }

  
}