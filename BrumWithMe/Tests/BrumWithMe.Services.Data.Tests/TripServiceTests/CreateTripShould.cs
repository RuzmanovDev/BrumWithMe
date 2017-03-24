using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Services.Providers.TimeProviders;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class CreateTripShould
    {
        [Test]
        public void CallCreateNewCity_WhenOriginCityFromTheInputIsNotPresentInTheDb()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            var tripCreatingInfo = new TripCreationInfo() { OriginName = "Sofia", DestinationName = "Pazardzhik" };

            City city = null;
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.OriginName))
                .Returns(city);

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo)).Returns(new Trip());

            // Act 
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.OriginName), Times.Once);
        }

        [Test]
        public void NotCallCityServiceCreateCity_WhenOriginCityExistsInDb()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            var tripCreatingInfo = new TripCreationInfo() { OriginName = "Sofia", DestinationName = "Pazardzhik" };

            City city = new City() { Name = tripCreatingInfo.OriginName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.OriginName))
                .Returns(city);

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo)).Returns(new Trip());

            // Act 
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.OriginName), Times.Never);
        }

        [Test]
        public void CallCreateCityFromCityRepo_WhenDestinationNameDoesNotExistInDb()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            var tripCreatingInfo = new TripCreationInfo() { OriginName = "Sofia", DestinationName = "Pazardzhik" };

            City city = null;
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.DestinationName))
                .Returns(city);

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo)).Returns(new Trip());

            // Act 
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.DestinationName), Times.Once);
        }

        [Test]
        public void NotCallCreateCityFrommCityService_WhenDestinationNameExistsInDb()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            var tripCreatingInfo = new TripCreationInfo() { OriginName = "Sofia", DestinationName = "Pazardzhik" };

            City city = new City() { Name = tripCreatingInfo.DestinationName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.DestinationName))
                .Returns(city);

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo)).Returns(new Trip());

            // Act 
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.DestinationName), Times.Never);
        }
        
        [Test]
        public void CallCreateCityFromCityServiceTwice_WhenOriginAndDestinationNamesDoesNotExistInDB()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            var tripCreatingInfo = new TripCreationInfo() { OriginName = "Sofia", DestinationName = "Pazardzhik" };

            City city = null;
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.DestinationName))
                .Returns(city);
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.OriginName))
            .Returns(city);

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo)).Returns(new Trip());

            // Act 
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.DestinationName), Times.Once);
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.OriginName), Times.Once);
        }

        [Test]
        public void NotCallCreateCityFromCityService_WhenOriginAndDestinationNamesExistInDB()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            var tripCreatingInfo = new TripCreationInfo() { OriginName = "Sofia", DestinationName = "Pazardzhik" };

            City originCity = new City() { Name = tripCreatingInfo.OriginName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.DestinationName))
                .Returns(originCity);

            City destinationCity = new City() { Name = tripCreatingInfo.DestinationName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.OriginName))
            .Returns(destinationCity);

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo)).Returns(new Trip());

            // Act 
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.DestinationName), Times.Never);
            mockedCityService.Verify(x => x.CreateCity(tripCreatingInfo.OriginName), Times.Never);
        }
    }
}
