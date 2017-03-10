using BrumWithMe.Web.Models.Trip;
using System.Collections.Generic;

namespace BrumWithMe.Web.Models.Search
{
    public class SearchTripResultViewModel
    {
        public SearchTripResultViewModel()
        {
            this.Trips = new List<TripBasicInfoViewModel>();
        }

        public IEnumerable<TripBasicInfoViewModel> Trips { get; set; }

        public SearchTripViewModel SearchModel { get; set; }
    }
}
