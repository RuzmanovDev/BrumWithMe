using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.ReviewServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithMessageContainingReviews_WhenReviewsRepoIsNull()
        {
            // Arrange
            IProjectableRepositoryEf<Review> reviews = null;
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new ReviewService(reviews, () => mockedUnitOfWork.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(reviews)));
        }

        [Test]
        public void ReturnAnInstance_AndNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.DoesNotThrow(() => new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object));
        }
    }
}
