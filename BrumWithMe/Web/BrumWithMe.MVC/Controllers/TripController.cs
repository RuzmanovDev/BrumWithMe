using BrumWithMe.Data;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Shared;
using BrumWithMe.Web.Models.Trip;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService tripService;
        private readonly ICityService cityService;
        private readonly ITagService tagService;
        private readonly ICarService carService;
        private readonly IMappingProvider mappingProvider;

        public TripController(
            ITripService tripService,
            ICityService cityService,
            ITagService tagService,
            ICarService carService,
            IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(tripService, nameof(tripService)).IsNull().Throw();
            Guard.WhenArgument(cityService, nameof(cityService)).IsNull().Throw();
            Guard.WhenArgument(tagService, nameof(tagService)).IsNull().Throw();
            Guard.WhenArgument(carService, nameof(carService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.tripService = tripService;
            this.cityService = cityService;
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
            // get from db pass it
            return PartialView("_RecentTrips");

        }

        [HttpPost]
        public ActionResult Create(CreateTripViewModel tripInfo)
        {
            if (!ModelState.IsValid)
            {
                return this.View(tripInfo);
            }

            var hourOfDeparture = TimeSpan.ParseExact(tripInfo.HourOfDeparture, @"hh\:mm", null);
            var timeOfDeparture = tripInfo.DateOfDeparture.Add(hourOfDeparture);
            var currentUSerId = this.User.Identity.GetUserId();

            var selectedTags = tripInfo.Tags.Where(x => x.IsSelected).Select(x => x.Id);

            var trip = this.mappingProvider.Map<CreateTripViewModel, TripCreationInfo>(tripInfo);

            trip.TimeOfDeparture = timeOfDeparture;
            trip.TagIds = selectedTags;
            trip.DriverId = currentUSerId;

            this.tripService.CreateTrip(trip);

            return RedirectToAction(nameof(this.Create));
        }

        public ActionResult Create()
        {
            var tags = this.tagService.GetAllTags();
            var userId = this.User.Identity.GetUserId();

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