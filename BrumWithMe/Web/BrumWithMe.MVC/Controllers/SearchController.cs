using BrumWithMe.Data;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Search;
using BrumWithMe.Web.Models.Trip;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly ITripService tripService;
        private readonly IMappingProvider mappingProvider;

        public SearchController(ITripService tripService, IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(tripService, nameof(tripService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.mappingProvider = mappingProvider;
            this.tripService = tripService;
        }

        public ActionResult Trips(SearchTripViewModel searchModel,string origin, string destination)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.View();
            //}

            //var trips = this.tripService.GetTripsFor(searhModel.Origin, searhModel.Destination);
            var trips = this.tripService.GetTripsFor(origin,destination);

            var recentTripsViewModel = this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(trips);

            var model = new SearchTripResultViewModel();
            model.SearchModel = searchModel;
            model.Trips = recentTripsViewModel;

            return View(model);
        }
    }
}