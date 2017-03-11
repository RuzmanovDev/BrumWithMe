using BrumWithMe.Web.Models.Trip;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrumWithMe.Web.Models.Search
{
    public class SearchTripViewModel
    {
        [Required]
        [MinLength(2)]
        public string Origin { get; set; }

        [Required]
        [MinLength(2)]
        public string Destination { get; set; }

        public IList<TripBasicInfoViewModel> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
