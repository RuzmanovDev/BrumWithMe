using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;
using BrumWithMe.Data.Models.CompositeModels.Review;
using System.Linq.Expressions;

namespace BrumWithMe.Services.Data.Tests.ReviewServiceTests
{
    [TestFixture]
    public class GetCommentsFor_Should
    {
        [Test]
        public void ThrowArgumenNulLException_WhenUserIdIsNull()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object);

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => service.GetCommentsFor(null, It.IsAny<int>()));
        }

        [Test]
        public void ThrowArgumentException_WhenUserIdIsEmpty()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => service.GetCommentsFor(string.Empty, It.IsAny<int>()));
        }

        [Test]
        public void ShouldCallGetAllMappedOnce_WithCorrectParams()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object);
            var page = 1;
            var userId = "userid";

            mockedReviewRepo.Setup(x => x.GetAllMappedWithDescSort<DateTime, CommentInfo>(It.IsAny<Expression<Func<Review, bool>>>(),
              It.IsAny<Expression<Func<Review, DateTime>>>(), page, 5));

            // Act
            service.GetCommentsFor(userId, page);

            // Assert
            mockedReviewRepo.Verify(x => x.GetAllMappedWithDescSort<DateTime, CommentInfo>(It.IsAny<Expression<Func<Review, bool>>>(),
               It.IsAny<Expression<Func<Review, DateTime>>>(), page, 5)
            , Times.Once);
        }
    }
}
