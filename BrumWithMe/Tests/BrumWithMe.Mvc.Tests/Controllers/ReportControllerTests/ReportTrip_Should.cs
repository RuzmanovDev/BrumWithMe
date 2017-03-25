using System.Linq;
using System.Web.Mvc;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.ReportControllerTests
{
    [TestFixture]
    public class ReportTrip_Should
    {
        [Test]
        public void Have_HttpPost_Attribute()
        {
            // Arrange
            var method = typeof(ReportController).GetMethod(nameof(ReportController.ReportTrip));

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void CallReportService_WithCorrectParams()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            var tripId = 1;
            var controller = new ReportController(mockedReportService.Object);

            // Act
            controller.ReportTrip(tripId);

            // Assert
            mockedReportService.Verify(x => x.ReportTrip(tripId), Times.Once);
        }
    }
}
