using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.ReportControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentExcpetion_WithMessaveContainingReportService_WhenReportServiceIsNull()
        {
            // Arrange
            IReportService reportService = null;

            // Act and Assert
            Assert.That(() => new ReportController(reportService),
                Throws.ArgumentNullException.With.Message.Contain(nameof(reportService)));
        }

        [Test]
        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new ReportController(mockedReportService.Object));
        }
    }
}
