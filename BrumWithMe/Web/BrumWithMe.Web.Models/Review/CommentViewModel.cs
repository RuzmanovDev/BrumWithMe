using BrumWithMe.Web.Models.Shared;
using System;

namespace BrumWithMe.Web.Models.Review
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public UserBannerViewModel Author { get; set; }

        public string Content { get; set; }

        public double Rating { get; set; }

        public DateTime PostedOn { get; set; }

    }
}
