using System.Collections.Generic;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.TripControllerTests
{
    [TestFixture]
    public class RecentTrips_Should
    {
        [Test]
        public void Call_RecentTripsPartial_WithDataFromTripService()
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

            var latestTripsCount = 6;
            var tripsBasicInfo = new List<TripBasicInfo>();
            mockedTripService.Setup(x => x.GetLatestTripsBasicInfo(latestTripsCount))
                .Returns(tripsBasicInfo);

            var tripsBasicInfoViewModel = new List<TripBasicInfoViewModel>();
            mockedMappingProvider.Setup(x => x.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(tripsBasicInfo))
                .Returns(tripsBasicInfoViewModel);

            // Act and Assert
            controller.WithCallTo(x => x.RecentTrips())
                .ShouldRenderPartialView("_RecentTrips")
                .WithModel(tripsBasicInfoViewModel);
        }
    }
}
