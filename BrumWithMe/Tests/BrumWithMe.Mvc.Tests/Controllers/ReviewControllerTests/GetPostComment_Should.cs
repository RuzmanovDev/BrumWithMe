using System;
using System.Linq;
using System.Web.Mvc;
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
    public class GetPostComment_Should
    {
        [Test]
        public void Have_Authorize_Attribute()
        {
            // Arrange
            var method = typeof(ReviewController).GetMethod(nameof(ReviewController.GetPostComment), new Type[] { typeof(string) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Return_EmptyResult_IfTheTripIdIsCreatedByTheLoggedUser()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            var controller = new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object);
            var loggedUserId = "LoggedUSerId";
            controller.GetLoggedUserId = () => loggedUserId;

            // Act and Assert
            controller.WithCallTo(x => x.GetPostComment(loggedUserId))
                .ShouldReturnEmptyResult();
        }

        [Test]
        public void Return_PostCommentPartial_WithCorrectViewModel()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            var controller = new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object);
            var loggedUserId = "LoggedUSerId";
            controller.GetLoggedUserId = () => loggedUserId;

            var userCreatedTheTrip = "userCreater1230";

            // Act and Assert
            controller.WithCallTo(x => x.GetPostComment(userCreatedTheTrip))
                .ShouldRenderPartialView("_PostComment")
                .WithModel<PostCommentViewModel>(x => x.ReviewedUserId == userCreatedTheTrip);
        }
    }
}
