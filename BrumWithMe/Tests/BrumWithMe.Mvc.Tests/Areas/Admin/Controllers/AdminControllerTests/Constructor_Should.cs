using System.Linq;
using System.Web.Mvc;
using BrumWithMe.MVC.Areas.Admin.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnAnInstanceOfController_WithAttrubuteAllowingOnlyAdmins()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            var mockedMapingProvider = new Mock<IMappingProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act
            var controller = new AdminController(mockedReportService.Object, mockedMapingProvider.Object, mockedTripService.Object);
            var attributes = typeof(AdminController).GetCustomAttributes(true);

            // Assert
            Assert.IsNotNull(attributes);
            Assert.AreEqual(1, attributes.Length);
            var authAttribute = (AuthorizeAttribute)attributes[0];
            string[] roles = authAttribute.Roles.Split(new char[] { ',' });
            Assert.IsTrue(roles.Contains("Admin"));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessagContainingReportService_WhenReportServiceIsNull()
        {
            // Arrange
            IReportService reportService = null;
            var mockedMapingProvider = new Mock<IMappingProvider>();
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            Assert.That(() => new AdminController(reportService, mockedMapingProvider.Object, mockedTripService.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(reportService)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingMappingProvider_WhenMappingProviderIsNull()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            IMappingProvider mappingProvider = null;
            var mockedTripService = new Mock<ITripService>();

            // Act and Assert
            Assert.That(() => new AdminController(mockedReportService.Object, mappingProvider, mockedTripService.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(mappingProvider)));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithMessageContainingTripService_WhenTripServiceIsNull()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            var mockedMapingProvider = new Mock<IMappingProvider>();
            ITripService tripService = null;

            // Act and Assert
            Assert.That(() => new AdminController(mockedReportService.Object, mockedMapingProvider.Object, tripService),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tripService)));
        }
    }
}
