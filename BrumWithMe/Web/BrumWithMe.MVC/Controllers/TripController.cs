using BrumWithMe.Data;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Services.Data.Contracts;
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
    public class TripController : Controller
    {
        private readonly ITripService tripService;
        private readonly ICityService cityService;
        private readonly ITagService tagService;
        private readonly ICarService carService;

        public TripController(ITripService tripService, ICityService cityService, ITagService tagService)
        {
            Guard.WhenArgument(tripService, nameof(tripService)).IsNull().Throw();
            Guard.WhenArgument(cityService, nameof(cityService)).IsNull().Throw();
            Guard.WhenArgument(tagService, nameof(tagService)).IsNull().Throw();

            this.tripService = tripService;
            this.cityService = cityService;
            this.tagService = tagService;
        }

        public ActionResult TripDetails(int id)
        {
            var db = new BrumWithMeDbContext();
            var model = new TripDetailsViewModel();
            var trip = db.Trips.Where(x => x.Id == id)
                .Select(x => new TripDetailsViewModel()
                {
                    OriginName = x.Origin.Name,
                    DestinationName = x.Destination.Name,
                    TimeOfDeparture = x.Date,
                    TakenSeats = x.TakenSeats,
                    TotalSeats = x.TotalSeats,
                    Price = x.Price,
                    DriverId = x.TripsUsers
                        .Where(z => z.TripId == x.Id && z.IsDriver)
                        .Select(z => z.UserId)
                        .FirstOrDefault(),
                    Description = x.Description,
                    Tags = x.Tags.Select(z => z.Name)
                })
                .FirstOrDefault(); ;

            return View(trip);
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

            var hourOfDeparting = TimeSpan.ParseExact(tripInfo.HourOfDeparture, @"hh\:mm", null);
            var travelDate = tripInfo.DateOfDeparture.Add(hourOfDeparting);
            var currentUSerId = this.User.Identity.GetUserId();
            var selectedTags = tripInfo.Tags.Where(x => x.IsSelected).Select(x => x.Id);

            var trip = new TripCreationInfo()
            {
                Description = tripInfo.Description,
                DestinationName = tripInfo.DestinationName,
                OriginName = tripInfo.OriginName,
                FreeSeats = tripInfo.FreeSeats,
                Price = tripInfo.Price,
                TagIds = selectedTags,
                TimeOfDeparture = travelDate,
                CarId = tripInfo.CarId,
                DriverId = currentUSerId
            };

            this.tripService.CreateTrip(trip);

            return RedirectToAction(nameof(this.Create));
        }

        public ActionResult Create()
        {
            var tags = this.tagService.GetAllTags();
            var userId = this.User.Identity.GetUserId();

            var cars = this.carService.GetUserCars(userId);

            var model = new CreateTripViewModel();

            var carsVModel = new List<CarViewModel>();
            foreach (var car in cars)
            {
                carsVModel.Add(new CarViewModel()
                {
                    Id = car.Id,
                    Name = car.Make
                });
            }


            var tagsvModel = new List<TagViewModel>();

            foreach (var ta in tags)
            {
                tagsvModel.Add(new TagViewModel()
                {
                    Id = ta.Id,
                    Name = ta.Name
                });
            }

            model.UserCars = carsVModel;
            model.Tags = tagsvModel;

            return View(model);
        }
    }


}