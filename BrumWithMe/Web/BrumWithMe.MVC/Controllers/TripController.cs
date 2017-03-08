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
            Guard.WhenArgument(tripService, nameof(tripService)).IsNull().Throw();
            Guard.WhenArgument(tagService, nameof(tagService)).IsNull().Throw();
            Guard.WhenArgument(carService, nameof(carService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.tripService = tripService;
            this.tagService = tagService;
            this.carService = carService;
            this.mappingProvider = mappingProvider;
        }

        public ActionResult TripDetails(int id)
        {
            TripDetails tripDetails = this.tripService.GetTripDetails(id);
            TripDetailsViewModel tripDetailsView = this.mappingProvider.Map<TripDetails, TripDetailsViewModel>(tripDetails);

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

            var selectedTags = tripInfo.Tags.Where(x => x.IsSelected).Select(x => x.Id);

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
            if (cars == null)
            {
                this.RedirectToAction(nameof(ManageController.RegisterCar), "Manage");
            }

            var model = new CreateTripViewModel();

            var carsVModel = this.mappingProvider.Map<IEnumerable<CarBasicInfo>, IEnumerable<CarViewModel>>(cars);
            var tagsvModel = this.mappingProvider.Map<IEnumerable<TagInfo>, IList<TagViewModel>>(tags);

            model.UserCars = carsVModel;
            model.Tags = tagsvModel;

            return View(model);
        }
    }
}