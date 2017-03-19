using System.Linq;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryEf<Review> reviews;

        public ReviewService(IRepositoryEf<Review> reviews)
        {
            Guard.WhenArgument(reviews, nameof(reviews)).IsNull().Throw();

            this.reviews = reviews;
        }

        public double GetAverageUserRating(string userId)
        {
            return this.reviews.GetAll(x => x.ReviewedUserId == userId, x => x.Rating).Average();
        }
    }
}
