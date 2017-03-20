using BrumWithMe.Data;
using BrumWithMe.Web.Models.Review;
using BrumWithMe.Web.Models.Shared;
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
        public ActionResult CommentsForUser(string userId, int page = 0)
        {
            if (this.TempData["page"] == null)
            {
                this.TempData["page"] = page;
            }
            else
            {
                page = int.Parse(this.TempData["page"].ToString());
                ++page;
                this.TempData["page"] = page;
            }

            var ctx = new BrumWithMeDbContext();

            var comments = ctx.DriverReviews.Where(x => x.ReviewedUserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new CommentViewModel()
                {
                    Author = new UserBannerViewModel()
                    {
                        AvataImageurl = x.Creator.AvataImageurl,
                        FullName = x.Creator.FirstName + x.Creator.LastName,
                        Rating = x.Creator.ReviewsForHim.Select(z => z.Rating).Average()
                    },
                    PostedOn = x.CreatedOn,
                    Content = x.Content,
                    Rating = x.Rating,
                    Id = x.Id
                })
                .Skip(page * 5)
                .Take(5)
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
}