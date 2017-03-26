using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BrumWithMe.Data.Models.CompositeModels.Review;
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
    public class CommentsForUser_Should
    {
        [Test]
        public void Have_Authorize_Attribute()
        {
            // Arrange
            var method = typeof(ReviewController).GetMethod(nameof(ReviewController.CommentsForUser), new Type[] { typeof(string), typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Return_CommentsPartial_WithCorrectViewModel()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            var controller = new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object);
            string userId = "userId";
            int page = 1;

            IEnumerable<CommentInfo> data = new List<CommentInfo>();
            mockedReviewService.Setup(x => x.GetCommentsFor(userId, page))
                .Returns(data);

            IEnumerable<CommentViewModel> comments = new List<CommentViewModel>();
            mockedMappingProvider.Setup(x => x.Map<IEnumerable<CommentInfo>, IEnumerable<CommentViewModel>>(data))
                .Returns(comments);

            // Act and Assert
            controller.WithCallTo(x => x.CommentsForUser(userId, page))
                .ShouldRenderPartialView("_Comment")
                .WithModel(comments);
        }
    }
}
