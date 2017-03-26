using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            // Act and Assert
            Assert.DoesNotThrow(() => new SearchController(mockedTripService.Object, mockedMappingProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingTirpService_WhenTripServiceIsNull()
        {
            // Arrange
            ITripService tripService = null;
            var mockedMappingProvider = new Mock<IMappingProvider>();

            // Act and Assert
            Assert.That(() => new SearchController(tripService, mockedMappingProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tripService)));
        }

        [Test]
        public void ThrowArgumentNullExcepion_WithMessageContainingMappingProvider_WhenMappingProviderIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            IMappingProvider mappingProvider = null;

            // Act and Assert
            Assert.That(() => new SearchController(mockedTripService.Object, mappingProvider),
                Throws.ArgumentNullException.With.Message.Contain(nameof(mappingProvider)));
        }
    }
}
