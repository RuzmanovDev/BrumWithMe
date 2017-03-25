using System.Collections.Generic;
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
    public class PassangersInfo_Should
    {
        [Test]
        public void test()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            var controller = new DashboardController(mockedTripService.Object, mockedUserDashboardService.Object);

            var loggedUserId = "loggedUser";
            controller.GetLoggedUserId = () => loggedUserId;

            var tripId = 1;
            var trips = new List<PassangerInfo>();
            mockedTripService.Setup(x => x.GetPassengersForTheTrip(tripId))
                .Returns(trips);

            // Act and assert
            controller.WithCallTo(x => x.PassangersInfo(tripId))
                .ShouldRenderPartialView("_Passangers")
                .WithModel(trips);
        }

        [Test]
        public void Have_HttpPost_Attribute()
        {
            // Arrange
            var method = typeof(DashboardController).GetMethod(nameof(DashboardController.PassangersInfo));

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }
    }
}
