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
    public class SignOutOftheTrip_Should
    {
        [Test]
        public void Have_HttpPostAttribute()
        {
            // Arrange
            var method = typeof(TripController).GetMethod(nameof(TripController.SignOutOftheTrip), new Type[] { typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Have_Authorize_Attribute()
        {
            // Arrange
            var method = typeof(TripController).GetMethod(nameof(TripController.SignOutOftheTrip), new Type[] { typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void AttachTrue_ToTheViewModel_WhenUserIsStillInTheTrip()
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

            int tripId = 1;
            string loggedUserId = "LoggedUsrId";
            controller.GetLoggedUserId = () => loggedUserId;

            var isIsuccesfullySignedOut = false;
            mockedTripService.Setup(x => x.SignOutOfTrip(tripId, loggedUserId))
                .Returns(isIsuccesfullySignedOut);

            bool IsUserInTheTrip = !isIsuccesfullySignedOut;

            // Act and Assert
            controller.WithCallTo(x => x.SignOutOftheTrip(tripId))
                .ShouldRenderPartialView("_JoinTripBtn")
                .WithModel<JoinTripBtnViewModel>(x => x.IsUserPassangerInTheTrip == IsUserInTheTrip && x.TripId == tripId);
        }

        [Test]
        public void AttachFalse_ToTheViewModel_WhenUserIsNotInTrip()
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

            int tripId = 1;
            string loggedUserId = "LoggedUsrId";
            controller.GetLoggedUserId = () => loggedUserId;

            var isIsuccesfullySignedOut = true;
            mockedTripService.Setup(x => x.SignOutOfTrip(tripId, loggedUserId))
                .Returns(isIsuccesfullySignedOut);

            bool IsUserInTheTrip = !isIsuccesfullySignedOut;

            // Act and Assert
            controller.WithCallTo(x => x.SignOutOftheTrip(tripId))
                .ShouldRenderPartialView("_JoinTripBtn")
                .WithModel<JoinTripBtnViewModel>(x => x.IsUserPassangerInTheTrip == IsUserInTheTrip && x.TripId == tripId);
        }
    }
}
