using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Web.Models.Trip
{
    public class CreateTripViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string DestinationName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string OriginName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public string HourOfDeparture { get; set; }

        public int FreeSeats { get; set; }

        public decimal Price { get; set; }

        public IList<TagViewModel> Tags { get; set; }

        public int CarId { get; set; }

        public IEnumerable<CarViewModel> UserCars { get; set; }
    }
}
