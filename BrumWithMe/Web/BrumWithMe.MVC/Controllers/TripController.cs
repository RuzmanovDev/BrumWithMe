using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class TripController : BaseController
    {
        private readonly ITripService tripService;
        private readonly ITagService tagService;
        private readonly ICarService carService;
        private readonly IMappingProvider mappingProvider;

        public TripController(
            ITripService tripService,
            ITagService tagService,
            ICarService carService,
            IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(tripService, nameof(ITripService)).IsNull().Throw();
            Guard.WhenArgument(tagService, nameof(ITagService)).IsNull().Throw();
            Guard.WhenArgument(carService, nameof(ICarService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(IMappingProvider)).IsNull().Throw();

            this.tripService = tripService;
            this.tagService = tagService;
            this.carService = carService;
            this.mappingProvider = mappingProvider;
        }

        public ActionResult TripDetails(int id)
        {
            TripDetails tripDetails = this.tripService.GetTripDetails(id);

            TripDetailsViewModel tripDetailsView = this.mappingProvider.Map<TripDetails, TripDetailsViewModel>(tripDetails);

            bool isUserAlreadyAppliedToTrip = false;

            if (base.GetLoggedUserId != null)
            {
                isUserAlreadyAppliedToTrip = this.tripService.isUserInTrip(base.GetLoggedUserId, id);
            }

            tripDetailsView.IsSeatReservedByCurrentUser = isUserAlreadyAppliedToTrip;

            return View(tripDetailsView);
        }

        public ActionResult RecentTrips()
        {
            var recentTrip = this.tripService.GetLatestTripsBasicInfo(6);

            var recentTripsViewModel = this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(recentTrip);

            return PartialView("_RecentTrips", recentTripsViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTripViewModel tripInfo)
        {
            if (!ModelState.IsValid)
            {
                return this.View(tripInfo);
            }

            var hourOfDeparture = TimeSpan.ParseExact(tripInfo.HourOfDeparture, @"hh\:mm", null);
            var timeOfDeparture = tripInfo.DateOfDeparture.Add(hourOfDeparture);
            var currentUSerId = base.GetLoggedUserId;


            var selectedTags = tripInfo.Tags
                ?.Where(x => x.IsSelected)
                ?.Select(x => x.Id);
            selectedTags = selectedTags ?? new List<int>();

            var trip = this.mappingProvider.Map<CreateTripViewModel, TripCreationInfo>(tripInfo);

            trip.TimeOfDeparture = timeOfDeparture;
            trip.TagIds = selectedTags;
            trip.DriverId = currentUSerId;

            this.tripService.CreateTrip(trip);

            return RedirectToAction(nameof(this.Create));
        }

        [Authorize]
        public ActionResult Create()
        {
            var tags = this.tagService.GetAllTags();
            var userId = base.GetLoggedUserId;

            var cars = this.carService.GetUserCars(userId);
            if (cars == null || cars.Count() < 1)
            {
                return this.RedirectToAction(nameof(ManageController.RegisterCar), "Manage");
            }

            var model = new CreateTripViewModel();

            var carsVModel = this.mappingProvider.Map<IEnumerable<CarBasicInfo>, IEnumerable<CarViewModel>>(cars);
            var tagsvModel = this.mappingProvider.Map<IEnumerable<TagInfo>, IList<TagViewModel>>(tags);

            model.UserCars = carsVModel;
            model.Tags = tagsvModel;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RequestToJoinTheTrip(int tripId, bool isUserOwner)
        {
            var userId = base.GetLoggedUserId;
            var isScucessfullyJoined = this.tripService.RequestToJoinTrip(tripId, userId);

            var model = new JoinTripBtnViewModel();
            model.IsUserOwner = isUserOwner;
            model.IsUserInTrip = isScucessfullyJoined;
            model.TripId = tripId;

            return this.PartialView("_JoinTripBtn", model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SignOutOftheTrip(int tripId, bool isUserOwner)
        {
            var userId = base.GetLoggedUserId;
            var result = this.tripService.SignOutOfTrip(tripId, userId);
            bool isUserInTrip = true;

            if (result)
            {
                isUserInTrip = false;
            }

            var model = new JoinTripBtnViewModel();
            model.IsUserOwner = isUserOwner;
            model.IsUserInTrip = isUserInTrip;
            model.TripId = tripId;

            return this.PartialView("_JoinTripBtn", model);
        }

        [ChildActionOnly]
        public ActionResult JoinBtn(string ownerId, bool isUserInTrip, int tripId)
        {
            var userId = base.GetLoggedUserId;
            bool isUserOwner = false;

            if (userId == ownerId)
            {
                isUserOwner = true;
            }

            var model = new JoinTripBtnViewModel();
            model.IsUserOwner = isUserOwner;
            model.IsUserInTrip = isUserInTrip;
            model.TripId = tripId;

            return this.PartialView("_JoinTripBtn", model);
        }

        [Authorize]
        public ActionResult DeleteTrip()
        {
            return null;
        }

    }

    public class JoinTripBtnViewModel
    {
        public bool IsUserOwner { get; set; }

        public bool IsUserInTrip { get; set; }

        public int TripId { get; set; }
    }
}