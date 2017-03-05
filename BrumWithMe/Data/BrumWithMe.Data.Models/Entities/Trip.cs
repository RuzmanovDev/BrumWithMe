using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.Entities
{
    public class Trip
    {
        private ICollection<User> passangers;
        private ICollection<Tag> tags;

        public Trip()
        {
            this.passangers = new HashSet<User>();
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public string DriverId { get; set; }

        [ForeignKey("DriverId")]
        public virtual User Driver { get; set; }

        public virtual ICollection<User> Passangers
        {
            get { return this.passangers; }
            set { this.passangers = value; }
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
