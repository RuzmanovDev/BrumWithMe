using System;

namespace BrumWithMe.Web.Models.Trip
{
    public class TripBasicInfoViewModel
    {
        public int Id { get; set; }

        public string OriginName { get; set; }

        public string DestinationName { get; set; }

        public int TotalSeats { get; set; }

        public int TakenSeats { get; set; }

        public decimal Price { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public string UserAvatarImageUrl { get; set; }
    }
}
