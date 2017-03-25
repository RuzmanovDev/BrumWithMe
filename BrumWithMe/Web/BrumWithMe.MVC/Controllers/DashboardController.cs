using System.Web.Mvc;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.MVC.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly ITripService tripService;
        private readonly IUserDashboardService userDashboardService;

        public DashboardController(ITripService tripService, IUserDashboardService userDashboardService)
        {
            Guard.WhenArgument(tripService, nameof(tripService)).IsNull().Throw();
            Guard.WhenArgument(userDashboardService, nameof(userDashboardService)).IsNull().Throw();

            this.tripService = tripService;
            this.userDashboardService = userDashboardService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TripsSharedByMe()
        {
            var loggedUserId = this.GetLoggedUserId();

            var data = this.userDashboardService.GetTripsCreatedByUser(loggedUserId);

            return this.PartialView("_AllTripsSharedByUser", data);
        }

        public ActionResult TripsJoinedByMe()
        {
            var userId = this.GetLoggedUserId();
            var trips = this.userDashboardService.GetTripsJoinedByUser(userId);

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
            var updatedTripInfo = this.userDashboardService.JoinUserToTrip(userId, tripId);

            return this.PartialView("_TripSharedByUserInfo", updatedTripInfo);
        }

        [HttpPost]
        public ActionResult RejectUserInTrip(string userId, int tripId)
        {
            var updatedTripInfo = this.userDashboardService.RejectUserToJoinTrip(userId, tripId);

            return this.PartialView("_TripSharedByUserInfo", updatedTripInfo);
        }
    }
}