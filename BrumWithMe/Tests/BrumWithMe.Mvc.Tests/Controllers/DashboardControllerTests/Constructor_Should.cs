using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.DashboardControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_ArgumentNullExcetpion_WithMessageContainTripService_WhenTripServiceIsNull()
        {
            // Arrange
            ITripService tripService = null;
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            // Act and Assert
            Assert.That(() => new DashboardController(tripService, mockedUserDashboardService.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tripService)));
        }

        [Test]
        public void Throw_ArgumentNullException_WithMessageContainUserDashboardService_WhenUserDashboardServiceIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            IUserDashboardService userDashboardService = null;

            // Act and Assert
            Assert.That(() => new DashboardController(mockedTripService.Object, userDashboardService),
                Throws.ArgumentNullException.With.Message.Contain(nameof(userDashboardService)));
        }

        [Test]
        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new DashboardController(mockedTripService.Object, mockedUserDashboardService.Object));
        }
    }
}
