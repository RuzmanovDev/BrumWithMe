using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrumWithMe.Data.Models.Entities
{
    public class Trip
    {
        private ICollection<UsersTrips> tripsUser;
        private ICollection<Tag> tags;

        public Trip()
        {
            this.tripsUser = new HashSet<UsersTrips>();
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public virtual ICollection<UsersTrips> TripsUsers
        {
            get { return this.tripsUser; }
            set { this.tripsUser = value; }
        }

        [Range(0,10000)]
        public decimal Price { get; set; }

        public int Seats { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual City Origin { get; set; }

        [ForeignKey("Origin")]
        public int OriginId { get; set; }

        public virtual City Destination { get; set; }

        [ForeignKey("Destination")]
        public int DestinationId { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public  virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
