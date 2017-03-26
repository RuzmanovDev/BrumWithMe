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
    public class JoinBtn_Should
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Retrun_JoinTripBtnPartial_WithCorrectlySetPropertiesToViewModel(bool isPassanger)
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

            mockedTripService.Setup(x => x.IsPassengerInTrip(loggedUserId, tripId))
                .Returns(isPassanger);

            // Act and Assert
            controller.WithCallToChild(x => x.JoinBtn(tripId))
                .ShouldRenderPartialView("_JoinTripBtn")
                .WithModel<JoinTripBtnViewModel>(x => x.IsUserPassangerInTheTrip == isPassanger && x.TripId == tripId);
        }
    }
}
