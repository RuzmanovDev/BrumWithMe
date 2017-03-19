using BrumWithMe.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Controllers
{
    public class ReviewController : Controller
    {
        public ActionResult CommentsForUser(string userId)
        {
            var ctx = new BrumWithMeDbContext();

            var comments = ctx.DriverReviews.Where(x => x.ReviewedUserId == userId)
                .OrderBy(x => x.CreatedOn)
                .Select(x => new CommentViewModel()
                {
                    AuthorId = x.CreatorId,
                    AuthroImageUrl = x.Creator.AvataImageurl,
                    PostedOn = x.CreatedOn,
                    Content = x.Content,
                    Rating = x.Rating
                })
                .ToList();

            return this.PartialView("_Comment", comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(PostCommentViewModel comment)
        {
            var ctx = new BrumWithMeDbContext();
            ctx.DriverReviews.Add(new Data.Models.Entities.Review()
            {
                Content = comment.Content,
                CreatedOn = DateTime.UtcNow,
                CreatorId = this.User.Identity.GetUserId(),
                Rating = comment.Rating,
                ReviewedUserId = comment.ReviewsUserId
            });

            ctx.SaveChanges();

            return this.CommentsForUser(comment.ReviewsUserId);
        }

        public ActionResult GetPostComment(string reviewFor)
        {
            var model = new PostCommentViewModel() { ReviewsUserId = reviewFor };
            return this.PartialView("_PostComment", model);
        }
    }

    public class CommentViewModel
    {
        public string AuthorId { get; set; }

        public string AuthroImageUrl { get; set; }

        public string Content { get; set; }

        public double Rating { get; set; }

        public DateTime PostedOn { get; set; }
    }

    public class PostCommentViewModel
    {
        public string Content { get; set; }

        public double Rating { get; set; }

        public string ReviewsUserId { get; set; }
    }
}