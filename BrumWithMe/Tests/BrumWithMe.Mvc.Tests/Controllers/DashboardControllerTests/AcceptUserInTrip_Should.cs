using System.Linq;
using System.Web.Mvc;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.DashboardControllerTests
{
    [TestFixture]
    public class AcceptUserInTrip_Should
    {
        [Test]
        public void Have_HttpPost_Attribute()
        {
            // Arrange
            var method = typeof(DashboardController).GetMethod(nameof(DashboardController.AcceptUserInTrip));

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Render_TripSharedByUserInfoPartial_WithCorrectDataFromTheService()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            var controller = new DashboardController(mockedTripService.Object, mockedUserDashboardService.Object);

            var userId = "userId";
            var tripId = 1;

            var data = new TripInfoWithUserRequests();
            mockedUserDashboardService.Setup(x => x.JoinUserToTrip(userId, tripId))
                .Returns(data);

            // Act and Assert
            controller.WithCallTo(x => x.AcceptUserInTrip(userId, tripId))
                .ShouldRenderPartialView("_TripSharedByUserInfo")
                .WithModel(data);
        }
    }
}
