using System;
using System.Linq;
using System.Web.Mvc;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Review;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.ReviewControllerTests
{
    [TestFixture]
    public class PostComment_Should
    {
        [Test]
        public void Have_HttpPost_Attribute()
        {
            // Arrange
            var method = typeof(ReviewController).GetMethod(nameof(ReviewController.PostComment), new Type[] { typeof(PostCommentViewModel) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Have_Authorize_Attribute()
        {
            // Arrange
            var method = typeof(ReviewController).GetMethod(nameof(ReviewController.PostComment), new Type[] { typeof(PostCommentViewModel) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Have_AntiforgeryTokenAttribute()
        {
            // Arrange
            var method = typeof(ReviewController).GetMethod(nameof(ReviewController.PostComment), new Type[] { typeof(PostCommentViewModel) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void ReturnEmpyResult_WhenModelStateIsNotValid()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            var controller = new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object);
            controller.ModelState.AddModelError("", "");

            var viewModel = new PostCommentViewModel();

            // Act and Assert
            controller.WithCallTo(x => x.PostComment(viewModel))
                .ShouldReturnEmptyResult();
        }

        [Test]
        public void Call_CreateReiew_IfCreatorId_IsDifferentThan_ReviewsUserId()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            var creatorId = "creator123";
            var reviewUserId = "reviewsuser123";

            var controller = new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object);
            controller.GetLoggedUserId = () => creatorId;

            var viewModel = new PostCommentViewModel();
            viewModel.ReviewedUserId = reviewUserId;

            var review = new Review();
            review.CreatorId = creatorId;
            review.ReviewedUserId = reviewUserId;
            mockedMappingProvider.Setup(x => x.Map<PostCommentViewModel, Review>(viewModel))
                .Returns(review);

            // Act and Assert
            controller.WithCallTo(x => x.PostComment(viewModel))
                .ShouldRenderPartialView("_Comment");

            mockedReviewService.Verify(x => x.CreateReview(review), Times.Once);
        }

        [Test]
        public void NotCall_CreateReiew_IfCreatorId_IsDifferentThan_ReviewsUserId()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            var creatorId = "creator123";

            var controller = new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object);
            controller.GetLoggedUserId = () => creatorId;

            var viewModel = new PostCommentViewModel();
            viewModel.ReviewedUserId = creatorId;

            var review = new Review();
            review.CreatorId = creatorId;
            review.ReviewedUserId = creatorId;
            mockedMappingProvider.Setup(x => x.Map<PostCommentViewModel, Review>(viewModel))
                .Returns(review);

            // Act and Assert
            controller.WithCallTo(x => x.PostComment(viewModel))
                .ShouldRenderPartialView("_Comment");

            mockedReviewService.Verify(x => x.CreateReview(review), Times.Never);
        }
    }
}
