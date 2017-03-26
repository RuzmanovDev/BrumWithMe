using System;
using System.Linq;
using System.Web.Mvc;
using BrumWithMe.MVC.Areas.Admin.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestFixture]
   public class DeleteTrip_Should
    {
        [Test]
        public void Have_HttpPost_Attribute()
        {
            // Arrange
            var method = typeof(AdminController).GetMethod(nameof(AdminController.DeleteTrip), new Type[] { typeof(int) });

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(HttpPostAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void Call_DeleteTrip_FromTripService_AndRedirectToReportedTrips()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            var mockedMapingProvider = new Mock<IMappingProvider>();
            var mockedTripService = new Mock<ITripService>();

            var controller = new AdminController(mockedReportService.Object, mockedMapingProvider.Object, mockedTripService.Object);
            int tripId = 1;

            // Act and Assert
            controller.WithCallTo(x => x.DeleteTrip(tripId))
                .ShouldRedirectTo(x => x.ReportedTrips);

            mockedTripService.Verify(x => x.DeleteTrip(tripId));
        }
    }
}
