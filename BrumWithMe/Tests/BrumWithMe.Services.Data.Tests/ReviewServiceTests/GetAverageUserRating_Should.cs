using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.ReviewServiceTests
{
    [TestFixture]
    public class GetAverageUserRating_Should
    {
        [Test]
        public void CallGetAllMethodOfReviewRepoOnce_AndReturnAverageRating()
        {
            // Arrange
            var mockedReviewRepo = new Mock<IProjectableRepositoryEf<Review>>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new ReviewService(mockedReviewRepo.Object, () => mockedUnitOfWork.Object);
            var userId = "userId";

            var data = new List<double>()
            {
               5, 4, 5, 5, 3
            };

            mockedReviewRepo.Setup(x => x.GetAll(It.IsAny<Expression<Func<Review, bool>>>(),
                   It.IsAny<Expression<Func<Review, double>>>()))
                   .Returns(data);

            var expectedRating = data.Average();

            // Act
            var result = service.GetAverageUserRating(userId);

            // Assert
            mockedReviewRepo.Verify(x => x.GetAll(It.IsAny<Expression<Func<Review, bool>>>(),
                It.IsAny<Expression<Func<Review, double>>>()),
                Times.Once);

            Assert.AreEqual(expectedRating, result);
        }
    }
}
