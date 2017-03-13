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
            var vm = this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(data);
            var model = new DashBoardViewModel();
            model.TripsCreatedByUser = vm;

            return View(model);
        }
    }

    public class DashBoardViewModel
    {
        public IEnumerable<TripBasicInfoViewModel> TripsCreatedByUser { get; set; }
    }
}