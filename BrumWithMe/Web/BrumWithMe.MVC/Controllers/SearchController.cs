using System.Collections.Generic;
using System.Web.Mvc;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Search;
using BrumWithMe.Web.Models.Trip;
using Bytes2you.Validation;

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

        public ActionResult LoadTrips(SearchTripViewModel model, int page = 0)
        {
            if (page < 0)
            {
                page *= -1;
            }

            var trips = this.tripService.GetTripsFor(model.Origin, model.Destination, page);
            IEnumerable<TripBasicInfoViewModel> tripsViewModel =
                this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(trips.FoundTrips);

            model.Data = tripsViewModel;
            model.TotalCount = trips.TotalTrips;

            return this.PartialView("_TripSearchResult", model);
        }


        public ActionResult Result(SearchTripViewModel searchModel)
        {
            var trips = this.tripService.GetTripsFor(searchModel.Origin, searchModel.Destination);

            IEnumerable<TripBasicInfoViewModel> tripsViewModel =
                this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(trips.FoundTrips);

            searchModel.Data = tripsViewModel;
            searchModel.TotalCount = trips.TotalTrips;

            return this.View(searchModel);
        }

        [ChildActionOnly]
        public ActionResult Search()
        {
            var searchModel = new SearchTripViewModel();

            return this.PartialView("_TripQuickSearch", searchModel);
        }

    }
}