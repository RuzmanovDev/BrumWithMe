using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Web.Models.Trip
{
    public class CreateTripViewModel
    {
        public string DestinationName { get; set; }

        public string OriginName { get; set; }

        public DateTime DateOfDeparture { get; set; }

        public DateTime HourOfDeparture { get; set; }

        public int FreeSeats { get; set; }

        public decimal Price { get; set; }

        public IList<TagViewModel> Tags { get; set; }

        public int CarId { get; set; }

        public IEnumerable<CarViewModel> UserCars { get; set; }
    }
}
