using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrumWithMe.Data.Models.Entities
{
    public class Tag
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
    }
}