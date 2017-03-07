using BrumWithMe.Data;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Web.Models.Trip;
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
        public ActionResult TripDetails()
        {
            return View();
        }

        public ActionResult RecentTrips()
        {
            // get from db pass it
            return PartialView("_RecentTrips");

        }

        [HttpPost]
        public ActionResult Create(CreateTripViewModel a)
        {
            var context = new BrumWithMeDbContext();

            var origin = context.Cities.FirstOrDefault(x => x.Name == a.OriginName);
            if (origin == null)
            {
                context.Cities.Add(new Data.Models.Entities.City()
                {
                    Name = a.OriginName
                });

                context.SaveChanges();
            }

            var destination = context.Cities.FirstOrDefault(x => x.Name == a.DestinationName);
            if (destination == null)
            {
                context.Cities.Add(new Data.Models.Entities.City()
                {
                    Name = a.DestinationName
                });

                context.SaveChanges();
            }

            var hourOfDeparting = TimeSpan.ParseExact(a.HourOfDeparture, @"hh\:mm", null);
            var travelDate = a.DateOfDeparture.Add(hourOfDeparting);

            var tagIds = a.Tags.Select(z => z.Id);
            var tags = context.Tags.Where(x => tagIds.Contains(x.Id)).ToList();

            var trip = new Trip()
            {
                CarId = a.CarId,
                Date = travelDate,
                Seats = a.FreeSeats,
                DestinationId = destination.Id,
                OriginId = origin.Id,
                Price = a.Price,
                Tags = tags,
                Description = a.Description
            };

            context.Trips.Add(trip);


            var currentUSerId = this.User.Identity.GetUserId();

            context.UsersTrips.Add(new UsersTrips()
            {
                Trip = trip,
                UserId = currentUSerId,
                IsDriver = true
            });

            context.SaveChanges();

            return RedirectToAction(nameof(this.Create));
        }

        public ActionResult Create()
        {
            var context = new BrumWithMeDbContext();

            var tags = context.Tags.ToList();
            var userId = this.User.Identity.GetUserId();
            var cars = context.Cars.Where(x => x.OwenerId == userId).ToList();

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