using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrumWithMe.Data.Models.Entities
{
    public class UsersTrips
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int TripId { get; set; }

        public bool IsOwner { get; set; }

        public virtual User User { get; set; }

        public virtual Trip Trip { get; set; }

        [ForeignKey("UserTripStatus")]
        public  int UserTripStatusId { get; set; }

        public virtual UserTripStatus UserTripStatus { get; set; }
    }
}
