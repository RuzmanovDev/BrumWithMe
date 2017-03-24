using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Services.Providers.TimeProviders;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_TripRepo_WhenTripRepoIsNull()
        {
            // Arrange
            IProjectableRepositoryEf<Trip> tripRepo = null;
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new TripService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                mockedCityService.Object,
                mockedMappingProvider.Object,
                mockedTagService.Object,
                tripRepo,
                mockedDateTimpeProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tripRepo)));
        }

        [Test]
        public void Throw_ArgumentNullExcpetion_WithMessageContaining_UserTripRepo_WhenUserTripRepoIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            IProjectableRepositoryEf<UsersTrips> userTripsRepo = null;
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new TripService(
                () => mockedUnitOfWork.Object,
                userTripsRepo,
                mockedCityService.Object,
                mockedMappingProvider.Object,
                mockedTagService.Object,
                mockedTripRepo.Object,
                mockedDateTimpeProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(userTripsRepo)));
        }

        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_CityService_WhenCityServiceIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            ICityService cityService = null;
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new TripService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                cityService,
                mockedMappingProvider.Object,
                mockedTagService.Object,
                mockedTripRepo.Object,
                mockedDateTimpeProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(cityService)));
        }

        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_TagService_WhenTripRepoIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            ITagService tagService = null;
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new TripService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                mockedCityService.Object,
                mockedMappingProvider.Object,
                tagService,
                mockedTripRepo.Object,
                mockedDateTimpeProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tagService)));
        }

        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_UserTripRepo_WhenUserTripRepoIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            IProjectableRepositoryEf<UsersTrips> userTripsRepo = null;
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new TripService(
                () => mockedUnitOfWork.Object,
                userTripsRepo,
                mockedCityService.Object,
                mockedMappingProvider.Object,
                mockedTagService.Object,
                mockedTripRepo.Object,
                mockedDateTimpeProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(userTripsRepo)));
        }

        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_DateTimeProvider_WhenDateTimeProviderIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            IDateTimeProvider dateTimeProvider = null;
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() => new TripService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                mockedCityService.Object,
                mockedMappingProvider.Object,
                mockedTagService.Object,
                mockedTripRepo.Object,
                dateTimeProvider),
                Throws.ArgumentNullException.With.Message.Contain(nameof(dateTimeProvider)));
        }

        [Test]
        public void ReturnAnInstance_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.DoesNotThrow(() => new TripService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                mockedCityService.Object,
                mockedMappingProvider.Object,
                mockedTagService.Object,
                mockedTripRepo.Object,
                mockedDateTimpeProvider.Object));
        }
    }
}
