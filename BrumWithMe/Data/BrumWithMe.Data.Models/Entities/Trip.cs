using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrumWithMe.Data.Models.Contracts;

namespace BrumWithMe.Data.Models.Entities
{
    public class Trip : IDeletableEntity
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

        [Required]
        [Range(1,20)]
        public int TotalSeats { get; set; }

        [Required]
        [Range(0, 20)]
        public int TakenSeats { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual City Origin { get; set; }

        [Required]
        [ForeignKey("Origin")]
        public int OriginId { get; set; }

        public virtual City Destination { get; set; }

        [Required]
        [ForeignKey("Destination")]
        public int DestinationId { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public  virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public bool IsDeleted { get; set; }
    }
}
