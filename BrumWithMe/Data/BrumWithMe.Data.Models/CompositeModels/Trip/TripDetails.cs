using System;
using System.Collections.Generic;
using BrumWithMe.Data.Models.Entities;

namespace BrumWithMe.Data.Models.CompositeModels.Trip
{
    public class TripDetails
    {
        public int Id { get; set; }

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public int TakenSeats { get; set; }

        public int TotalSeats { get; set; }

        public decimal Price { get; set; }

        public UserBasicInfo Driver { get; set; }

        public string Description { get; set; }

        public IEnumerable<TagInfo> Tags { get; set; }

        public CarBasicInfo Car { get; set; }

        public bool IsDeleted { get; set; }
    }
}
