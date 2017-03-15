using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
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

            var model = new DashBoardViewModel();
            model.TripsCreatedByUser = data;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AcceptUserInTrip(string userId, int tripId)
        {
            var updatedTripInfo = this.tripService.JoinUserToTrip(userId, tripId);

            return this.PartialView("_TripsSharedByCurrentUser", updatedTripInfo);
        }
    }

    public class DashBoardViewModel
    {
        public IEnumerable<TripInfoWithUserRequests> TripsCreatedByUser { get; set; }
    }
}