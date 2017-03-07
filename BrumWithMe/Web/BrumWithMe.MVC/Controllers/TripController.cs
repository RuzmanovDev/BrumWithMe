using BrumWithMe.Data;
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

            return new EmptyResult();
        }

        public ActionResult Create()
        {
            var context = new BrumWithMeDbContext();

            var tags = context.Tags.ToList();
            var userId = this.User.Identity.GetUserId();
            var cars = context.Cars.Where(x => x.OwenerId == userId).ToList();

            var model = new CreateTripViewModel();

            var carsVModel= new List<CarViewModel>();
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