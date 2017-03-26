using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.TripControllerTests
{
    [TestFixture]
    public class RequestToJoinTheTrip_Should
    {
        [Test]
        public void Have_HttpPost_Attribute()
        {
            // Arrange
            var method = typeof(TripController).GetMethod(nameof(TripController.RequestToJoinTheTrip), new Type[] { typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Have_Authorize_Attribute()
        {
            // Arrange
            var method = typeof(TripController).GetMethod(nameof(TripController.RequestToJoinTheTrip), new Type[] { typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void ShouldAttachTrueToIssPassangerIntTrip_ToTheVModel_AndRender_JoinTripBtn_WhenTheServiceReturnsTrue()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new TripController(
                     mockedTripService.Object,
                     mockedTagService.Object,
                     mockedCarService.Object,
                     mockedMappingProvider.Object);

            string loggedUserId = "LoggedusrId";
            int tripId = 1;
            controller.GetLoggedUserId = () => loggedUserId;

            bool isSuccessfullyJoined = true;
            mockedTripService.Setup(x => x.RequestToJoinTrip(tripId, loggedUserId))
                .Returns(isSuccessfullyJoined);

            // Act and Assert
            controller.WithCallTo(x => x.RequestToJoinTheTrip(tripId))
                .ShouldRenderPartialView("_JoinTripBtn")
                .WithModel<JoinTripBtnViewModel>(x => x.TripId == tripId && x.IsUserPassangerInTheTrip == isSuccessfullyJoined);
        }


        [Test]
        public void ShouldAttachFalseToIssPassangerIntTrip_ToTheVModel_AndRender_JoinTripBtn_WhenTheServiceReturnsFalse()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new TripController(
                     mockedTripService.Object,
                     mockedTagService.Object,
                     mockedCarService.Object,
                     mockedMappingProvider.Object);

            string loggedUserId = "LoggedusrId";
            int tripId = 1;
            controller.GetLoggedUserId = () => loggedUserId;

            bool isSuccessfullyJoined = false;
            mockedTripService.Setup(x => x.RequestToJoinTrip(tripId, loggedUserId))
                .Returns(isSuccessfullyJoined);

            // Act and Assert
            controller.WithCallTo(x => x.RequestToJoinTheTrip(tripId))
                .ShouldRenderPartialView("_JoinTripBtn")
                .WithModel<JoinTripBtnViewModel>(x => x.TripId == tripId && x.IsUserPassangerInTheTrip == isSuccessfullyJoined);
        }
    }
}
