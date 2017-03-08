using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrumWithMe.Data.Models.Contracts;

namespace BrumWithMe.Data.Models.Entities
{
    public class Subscription : IDeletableEntity
    {
        [Key, Column(Order = 0), ForeignKey("UserToSubscribeTo")]
        public string UserToSubscribeToId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Subscriber")]
        public string SubscriberId { get; set; }

        public User UserToSubscribeTo { get; set; }

        public User Subscriber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
