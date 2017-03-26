using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.TripControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            // Act and Assert
            Assert.DoesNotThrow(
                () => new TripController(
                    mockedTripService.Object,
                    mockedTagService.Object,
                    mockedCarService.Object,
                    mockedMappingProvider.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContaining_TripService_WhenTripServiceIsNull()
        {
            // Arrange
            ITripService tripService = null;
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            // Act and Assert
            Assert.That(
                () => new TripController(
                    tripService,
                    mockedTagService.Object,
                    mockedCarService.Object,
                    mockedMappingProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tripService)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContaining_TagService_WhenTagServiceIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            ITagService tagService = null;
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            // Act and Assert
            Assert.That(
                () => new TripController(
                    mockedTripService.Object,
                    tagService,
                    mockedCarService.Object,
                    mockedMappingProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tagService)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContaining_CarService_WhenCarServiceIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            ICarService carService = null;
            var mockedMappingProvider = new Mock<IMappingProvider>();

            // Act and Assert
            Assert.That(
                () => new TripController(
                    mockedTripService.Object,
                    mockedTagService.Object,
                    carService,
                    mockedMappingProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(carService)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingMappingProvider_WhenMappingProviderIsNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            IMappingProvider mappingProvider = null;

            // Act and Assert
            Assert.That(
                () => new TripController(
                    mockedTripService.Object,
                    mockedTagService.Object,
                    mockedCarService.Object,
                    mappingProvider),
                Throws.ArgumentNullException.With.Message.Contain(nameof(mappingProvider)));
        }
    }
}
