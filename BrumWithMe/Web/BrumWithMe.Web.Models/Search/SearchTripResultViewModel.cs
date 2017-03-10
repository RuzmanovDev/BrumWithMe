using BrumWithMe.Web.Models.Trip;
using System.Collections.Generic;

namespace BrumWithMe.Web.Models.Search
{
    public class SearchTripResultViewModel
    {
        public IEnumerable<TripBasicInfoViewModel> Trips { get; set; }

        public SearchTripViewModel SearchModel { get; set; }
    }
}
