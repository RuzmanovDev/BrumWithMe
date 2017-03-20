using System.Collections.Generic;
using BrumWithMe.Data.Models.CompositeModels.Review;
using BrumWithMe.Data.Models.Entities;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface IReviewService
    {
        double GetAverageUserRating(string userId);

        void CreateReview(Review review);

        IEnumerable<CommentInfo> GetCommentsFor(string userId, int page);
    }
}
