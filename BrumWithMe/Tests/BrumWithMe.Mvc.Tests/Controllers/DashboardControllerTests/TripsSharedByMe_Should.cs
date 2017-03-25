using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.DashboardControllerTests
{
    [TestFixture]
    public class TripsSharedByMe_Should
    {
        [Test]
        public void RenderAllTripsSharedByUserPartialView_WithCorrectData()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            var controller = new DashboardController(mockedTripService.Object, mockedUserDashboardService.Object);

            var loggedUserId = "loggedUser";
            controller.GetLoggedUserId = () => loggedUserId;

            var data = new List<TripInfoWithUserRequests>();
            mockedUserDashboardService.Setup(x => x.GetTripsCreatedByUser(loggedUserId))
                .Returns(data);

            controller.WithCallTo(x => x.TripsSharedByMe())
                .ShouldRenderPartialView("_AllTripsSharedByUser")
                .WithModel(data);
        }
    }
}
