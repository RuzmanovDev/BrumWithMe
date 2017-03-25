using System.Collections.Generic;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.DashboardControllerTests
{
    [TestFixture]
    public class TripsJoinedByMe_Should
    {
        [Test]
        public void Render_TripsJoinedByUserPartial_WithCorrectData()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            var controller = new DashboardController(mockedTripService.Object, mockedUserDashboardService.Object);

            var loggedUserId = "loggedUser";
            controller.GetLoggedUserId = () => loggedUserId;

            var data = new List<TripBasicInfoWithStatus>();
            mockedUserDashboardService.Setup(x => x.GetTripsJoinedByUser(loggedUserId))
                .Returns(data);

            controller.WithCallTo(x => x.TripsJoinedByMe())
                .ShouldRenderPartialView("_TripsJoinedByUser")
                .WithModel(data);
        }
    }
}
