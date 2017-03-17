using System;
using System.Collections.Generic;
using BrumWithMe.Web.Models.Shared;

namespace BrumWithMe.Web.Models.Trip
{
    public class TripDetailsViewModel
    {
        public int Id { get; set; }

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public int TakenSeats { get; set; }

        public int TotalSeats { get; set; }

        public decimal Price { get; set; }

        public UserBannerViewModel Driver { get; set; }

        public string Description { get; set; }

        public CarViewModel Car{ get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }

        public bool IsCurrentUserPassangerInTheTrip { get; set; }
        public bool IsCurrentUserOwner{ get; set; }
    }
}
