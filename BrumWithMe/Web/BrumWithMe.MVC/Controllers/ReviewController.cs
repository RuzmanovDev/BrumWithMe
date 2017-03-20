using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BrumWithMe.Data.Models.CompositeModels.Review;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Review;
using Bytes2you.Validation;

namespace BrumWithMe.MVC.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IMappingProvider mappingProvider;
        private readonly IReviewService reviewService;

        public ReviewController(IMappingProvider mappingProvider, IReviewService reviewService)
        {
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();
            Guard.WhenArgument(reviewService, nameof(reviewService)).IsNull().Throw();

            this.mappingProvider = mappingProvider;
            this.reviewService = reviewService;
        }

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

            IEnumerable<CommentInfo> data = this.reviewService.GetCommentsFor(userId, page);

            IEnumerable<CommentViewModel> comments =
                this.mappingProvider.Map<IEnumerable<CommentInfo>, IEnumerable<CommentViewModel>>(data);

            return this.PartialView("_Comment", comments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(PostCommentViewModel comment)
        {
            if (!this.ModelState.IsValid)
            {
                // TODO handle errors and wrtie validation logic
                return null;
            }

            var review = this.mappingProvider.Map<PostCommentViewModel, Review>(comment);
            review.CreatorId = base.GetLoggedUserId;
            review.CreatedOn = DateTime.UtcNow;

            this.reviewService.CreateReview(review);

            return this.CommentsForUser(comment.ReviewedUserId);
        }

        public ActionResult GetPostComment(string reviewFor)
        {
            var model = new PostCommentViewModel() { ReviewedUserId = reviewFor };
            return this.PartialView("_PostComment", model);
        }
    }
}