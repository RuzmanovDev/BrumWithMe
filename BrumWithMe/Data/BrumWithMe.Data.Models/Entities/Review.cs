using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BrumWithMe.Data.Models.Contracts;

namespace BrumWithMe.Data.Models.Entities
{
    public class Review : IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string Content { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey("ReviewedUser")]
        public string ReviewedUserId { get; set; }

        public virtual User ReviewedUser { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
