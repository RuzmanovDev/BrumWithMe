namespace BrumWithMe.Web.Models.Trip
{
    public class JoinTripBtnViewModel
    {
        public bool IsUserOwner { get; set; }

        public bool IsUserInTrip { get; set; }

        public int TripId { get; set; }
    }
}
