using BrumWithMe.Data.Models.Entities;
using System;
using System.Collections.Generic;

namespace BrumWithMe.Data.Models.CompositeModels.Trip
{
    public class TripInfoWithUserRequests
    {
        public int Id { get; set; }

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public int TotalSeats { get; set; }

        public int TakenSeats { get; set; }

        public decimal Price { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public string CarAvatarImage { get; set; }

        public IEnumerable<PassangerInfo> Passangers { get; set; }
    }
}
