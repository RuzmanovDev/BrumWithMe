using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Web.Models.Trip
{
    public class TripDetailsViewModel
    {
        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public int TakenSeats { get; set; }

        public int TotalSeats { get; set; }

        public decimal Price { get; set; }

        public string DriverId { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
