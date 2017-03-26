using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
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
    public class MarkTripAsFinished_Should
    {
        [Test]
        public void Have_Authorize_Attribute()
        {
            // Arrange
            var method = typeof(TripController).GetMethod(nameof(TripController.MarkTripAsFinished), new Type[] { typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Redirect_ToTripDetails_WhenTripIs_NotMarkedAsFinishedSuccesfully()
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

            mockedTripService.Setup(x => x.MarkTripAsFinished(tripId, loggedUserId))
                .Returns(false);

            // Act and Assert
            controller.WithCallTo(x => x.MarkTripAsFinished(tripId))
                .ShouldRedirectTo<TripController>(x => x.TripDetails(tripId));
        }

        [Test]
        public void Redirect_ToIndex_WhenTripIs_MarkedAsFinishedSuccesfully()
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

            mockedTripService.Setup(x => x.MarkTripAsFinished(tripId, loggedUserId))
                .Returns(true);

            // Act and Assert
            controller.WithCallTo(x => x.MarkTripAsFinished(tripId))
                .ShouldRedirectTo<HomeController>(x => x.Index(0));
        }
    }
}
