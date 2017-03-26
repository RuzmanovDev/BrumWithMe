using BrumWithMe.MVC.Areas.Admin.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestFixture]
    public class Dashboard_Should
    {
        [Test]
        public void RetrunDefaulView()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            var mockedMapingProvider = new Mock<IMappingProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act
            var controller = new AdminController(mockedReportService.Object, mockedMapingProvider.Object, mockedTripService.Object);

            // Assert
            controller.WithCallTo(x => x.Dashboard())
                .ShouldRenderDefaultView();
        }
    }
}
