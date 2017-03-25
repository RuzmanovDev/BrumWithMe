using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNulLException_WhenAccountManagementService_IsNull()
        {
            // Arrange
            IAccountManagementService accountManagementService = null;

            // Act and Assert
            Assert.That(() => new HomeController(accountManagementService),
                Throws.ArgumentNullException.With.Message.Contain(nameof(accountManagementService)));
        }

        [Test]
        public void DoesNotThrow_WhenAllArugmentsAreValdi()
        {
            // Arrange
            var mockedAccountManagementService = new Mock<IAccountManagementService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new HomeController(mockedAccountManagementService.Object));
        }
    }
}
