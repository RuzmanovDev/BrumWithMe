using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrumWithMe.Data.Models.Contracts;

namespace BrumWithMe.Data.Models.Entities
{
    public class Tag : IDeletableEntity
    {
        private ICollection<Trip> trips;

        public Tag()
        {
            this.trips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }

            set { this.trips = value; }
        }

        public bool IsDeleted { get; set; }
    }
}