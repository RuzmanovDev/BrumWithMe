using System;
using System.Collections.Generic;
using System.Linq;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Review;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public class ReviewService : BaseDataService, IReviewService
    {
        private readonly IProjectableRepositoryEf<Review> reviews;

        public ReviewService(IProjectableRepositoryEf<Review> reviews, Func<IUnitOfWorkEF> unitOfWork)
            : base(unitOfWork)
        {
            Guard.WhenArgument(reviews, nameof(reviews)).IsNull().Throw();

            this.reviews = reviews;
        }

        public void CreateReview(Review review)
        {
            Guard.WhenArgument(review, nameof(review)).IsNull().Throw();

            using (var uow = base.UnitOfWork())
            {
                this.reviews.Add(review);

                uow.Commit();
            }
        }

        public double GetAverageUserRating(string userId)
        {
            return this.reviews.GetAll(x => x.ReviewedUserId == userId, x => x.Rating).Average();
        }

        public IEnumerable<CommentInfo> GetCommentsFor(string userId, int page)
        {
            Guard.WhenArgument(userId, nameof(userId)).IsNullOrEmpty().Throw();

            if (page < 0)
            {
                page = page * -1;
            }

            var result = this.reviews.GetAllMapped<DateTime, CommentInfo>(x => x.ReviewedUserId == userId, sort => sort.CreatedOn, page, 5);

            return result;
        }
    }
}
