using BrumWithMe.Data.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrumWithMe.Data.Models.Entities
{
    public class Car : IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string Color { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        public string Make { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Range(1900, 3000)]
        public int Year { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [ForeignKey("Owner")]
        public string OwenerId { get; set; }

        public virtual User Owner { get; set; }

        public bool IsDeleted { get; set; }
    }
}
