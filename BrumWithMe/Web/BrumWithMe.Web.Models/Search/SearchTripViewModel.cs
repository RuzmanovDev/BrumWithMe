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
    }
}
