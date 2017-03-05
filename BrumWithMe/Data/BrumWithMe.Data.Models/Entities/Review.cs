using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Data.Models.Entities
{
    public class Review
    {
        //private ICollection<ReviewsUsers> reviewers;

        public Review()
        {
            //this.reviewers = new HashSet<ReviewsUsers>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string Content { get; set; }

        [Range(0, 5)]
        [Required]
        public double Rating { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        //public virtual ICollection<ReviewsUsers> Reviewers
        //{
        //    get { return this.reviewers; }

        //    set { this.reviewers = value; }
        //}

        [ForeignKey("ReviewedUser")]
        [Required]
        public string ReviewedUserId { get; set; }

        public virtual User ReviewedUser { get; set; }

        public virtual User Creator { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public string CreatorId { get; set; }
    }
}
