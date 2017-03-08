using BrumWithMe.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.TransportEntities
{
    public class TripCreationInfo
    {
        public City Origin { get; set; }
        public City Destination { get; set; }

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public string Description { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public int TotalSeats { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<int> TagIds { get; set; }

        public int CarId { get; set; }

        public string DriverId { get; set; }
    }
}
