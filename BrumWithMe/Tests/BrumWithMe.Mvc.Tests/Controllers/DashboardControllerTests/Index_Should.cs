using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.DashboardControllerTests
{
    [TestFixture]
   public class Index_Should
    {
        [Test]
        public void RenderDefaultView()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedUserDashboardService = new Mock<IUserDashboardService>();

            var controller = new DashboardController(mockedTripService.Object, mockedUserDashboardService.Object);

            // Act and Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}
