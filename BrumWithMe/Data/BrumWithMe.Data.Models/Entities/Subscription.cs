using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrumWithMe.Data.Models.Entities
{
    public class Subscription
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Key, Column(Order = 1), ForeignKey("UserToSubscribeTo")]
        public string UserToSubscribeToId { get; set; }

        [ForeignKey("Subscriber")]
        public string SubscriberId { get; set; }

        public User UserToSubscribeTo { get; set; }

        public User Subscriber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
