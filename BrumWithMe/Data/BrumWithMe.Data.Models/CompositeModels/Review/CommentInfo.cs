using System;

namespace BrumWithMe.Data.Models.CompositeModels.Review
{
    public class CommentInfo
    {
        public int Id { get; set; }

        public UserBasicInfo Author { get; set; }

        public string Content { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
