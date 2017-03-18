using System.Web.Mvc;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
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
            return View();
        }

        public ActionResult TripsSharedByMe()
        {
            var loggedUserId = this.User.Identity.GetUserId();

            var data = this.tripService.GetTripsCreatedByUser(loggedUserId);

            return this.PartialView("_AllTripsSharedByUser", data);
        }

        public ActionResult TripsJoinedByMe()
        {
            var userId = base.GetLoggedUserId;
            var trips = this.tripService.GetTripsJoinedByUser(userId);

            return this.PartialView("_TripsJoinedByUser", trips);
        }

        [HttpPost]
        public ActionResult PassangersInfo(int tripId)
        {
            var passangers = this.tripService.GetPassengersForTheTrip(tripId);

            return this.PartialView("_Passangers", passangers);
        }

        [HttpPost]
        public ActionResult AcceptUserInTrip(string userId, int tripId)
        {
            var updatedTripInfo = this.tripService.JoinUserToTrip(userId, tripId);

            return this.PartialView("_TripSharedByUserInfo", updatedTripInfo);
        }

        [HttpPost]
        public ActionResult RejectUserInTrip(string userId, int tripId)
        {
            var updatedTripInfo = this.tripService.RejectUserToJoinTrip(userId, tripId);

            return this.PartialView("_TripSharedByUserInfo", updatedTripInfo);
        }
    }
}