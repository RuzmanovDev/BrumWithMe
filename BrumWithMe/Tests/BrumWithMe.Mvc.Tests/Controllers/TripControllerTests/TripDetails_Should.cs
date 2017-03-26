using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Shared;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.TripControllerTests
{
    [TestFixture]
    public class TripDetails_Should
    {
        [Test]
        public void SetIsUserOwner_ToTrue_IfTheUserIsOwnerOfTheTrip()
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
            var driverId = "UserId";

            var tripDetails = new TripDetails() { Id = tripId };

            mockedTripService.Setup(x => x.GetTripDetails(tripId))
                .Returns(tripDetails);

            var tripDetailsViewModel = new TripDetailsViewModel();
            tripDetailsViewModel.Driver = new UserBannerViewModel() { Id = driverId };
            mockedMappingProvider.Setup(x => x.Map<TripDetails, TripDetailsViewModel>(tripDetails))
                .Returns(tripDetailsViewModel);

            controller.GetLoggedUserId = () => driverId;

            mockedTripService.Setup(x => x.IsPassengerInTrip(driverId, tripId))
                .Returns(false);

            // Act and Assert
            controller.WithCallTo(x => x.TripDetails(tripId))
                .ShouldRenderDefaultView()
                .WithModel(tripDetailsViewModel);

            Assert.IsTrue(tripDetailsViewModel.IsCurrentUserOwner);
            Assert.IsFalse(tripDetailsViewModel.IsCurrentUserPassangerInTheTrip);
        }

        [Test]
        public void SetIsUserPassangerToTrue_IfUSerIsPassanger()
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
            var driverId = "UserId";
            var loggedUserId = "loggedUserId";
            var tripDetails = new TripDetails() { Id = tripId };

            mockedTripService.Setup(x => x.GetTripDetails(tripId))
                .Returns(tripDetails);

            var tripDetailsViewModel = new TripDetailsViewModel();
            tripDetailsViewModel.Driver = new UserBannerViewModel() { Id = driverId };
            mockedMappingProvider.Setup(x => x.Map<TripDetails, TripDetailsViewModel>(tripDetails))
                .Returns(tripDetailsViewModel);

            controller.GetLoggedUserId = () => loggedUserId;

            mockedTripService.Setup(x => x.IsPassengerInTrip(loggedUserId, tripId))
                .Returns(true);

            // Act and Assert
            controller.WithCallTo(x => x.TripDetails(tripId))
                .ShouldRenderDefaultView()
                .WithModel(tripDetailsViewModel);

            Assert.IsFalse(tripDetailsViewModel.IsCurrentUserOwner);
            Assert.IsTrue(tripDetailsViewModel.IsCurrentUserPassangerInTheTrip);
        }

        [Test]
        public void SetIsPassangerToFalse_AndIsUserOwnerToFalse_WhenThereIsNoLoggedUSer()
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
            var driverId = "UserId";
            string loggedUserId = null;
            var tripDetails = new TripDetails() { Id = tripId };

            mockedTripService.Setup(x => x.GetTripDetails(tripId))
                .Returns(tripDetails);

            var tripDetailsViewModel = new TripDetailsViewModel();
            tripDetailsViewModel.Driver = new UserBannerViewModel() { Id = driverId };
            mockedMappingProvider.Setup(x => x.Map<TripDetails, TripDetailsViewModel>(tripDetails))
                .Returns(tripDetailsViewModel);

            controller.GetLoggedUserId = () => loggedUserId;

            // Act and Assert
            controller.WithCallTo(x => x.TripDetails(tripId))
                .ShouldRenderDefaultView()
                .WithModel(tripDetailsViewModel);

            mockedTripService.Verify(x => x.IsPassengerInTrip(loggedUserId,tripId), 
                Times.Never);

            Assert.IsFalse(tripDetailsViewModel.IsCurrentUserOwner);
            Assert.IsFalse(tripDetailsViewModel.IsCurrentUserPassangerInTheTrip);
        }
    }
}
