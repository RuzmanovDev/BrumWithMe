using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.ReviewServiceTests
{
    [TestFixture]
    public class CreateReview_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithMessageContainingReview_WhenReviewIsNull()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object);

            Review review = null;

            // Act and Assert
            Assert.That(() => service.CreateReview(review),
                Throws.ArgumentNullException.With.Message.Contain(nameof(review)));
        }

        [Test]
        public void CallAddMethodFromTheReviewRepo_OnceWithPassedAsparameterReview()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object);

            Review review = new Review();

            // Act
            service.CreateReview(review);

            // Assert
            mockedReviewRepo.Verify(x => x.Add(review), Times.Once);
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
