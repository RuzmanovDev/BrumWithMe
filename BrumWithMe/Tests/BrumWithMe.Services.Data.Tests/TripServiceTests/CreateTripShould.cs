using System;
using System.Collections.Generic;
using System.Linq;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.Enums;
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

        [Test]
        public void CreateTripWithoutTags_WhenThereAreNotagsProvider()
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

            var tripCreatingInfo = new TripCreationInfo()
            {
                OriginName = "Sofia",
                DestinationName = "Pazardzhik",
                TagIds = new List<int>()
            };

            City originCity = new City() { Name = tripCreatingInfo.OriginName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.DestinationName))
                .Returns(originCity);

            City destinationCity = new City() { Name = tripCreatingInfo.DestinationName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.OriginName))
            .Returns(destinationCity);

            var foundTags = new List<Tag>();
            mockedTagService.Setup(x => x.GetTagsByIds(tripCreatingInfo.TagIds)).Returns(foundTags);

            var trip = new Trip()
            {
                Tags = foundTags
            };

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo))
                .Returns(trip);

            // Act
            tripService.CreateTrip(tripCreatingInfo);

            // Assert
            mockedTripRepo.Verify(x => x.Add(trip), Times.Once);
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void CreateTripWithAllPropertiesFromMethodParameter()
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

            var tripCreatingInfo = new TripCreationInfo()
            {
                OriginName = "Sofia",
                DestinationName = "Pazardzhik",
                TagIds = new List<int>(),
                Description = "test",
                Price = 4,
                CarId = 1,
                TotalSeats = 2,
                TimeOfDeparture = DateTime.Now,
                DriverId = "1"
            };

            City originCity = new City() { Name = tripCreatingInfo.OriginName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.DestinationName))
                .Returns(originCity);

            City destinationCity = new City() { Name = tripCreatingInfo.DestinationName };
            mockedCityService.Setup(x => x.GetCityByName(tripCreatingInfo.OriginName))
            .Returns(destinationCity);

            var foundTags = new List<Tag>();
            mockedTagService.Setup(x => x.GetTagsByIds(tripCreatingInfo.TagIds)).Returns(foundTags);

            var dateCreated = DateTime.Now;
            mockedDateTimpeProvider.Setup(x => x.Now).Returns(dateCreated);

            var trip = new Trip()
            {
                Tags = foundTags,
                Origin = originCity,
                Destination = destinationCity,
                CarId = tripCreatingInfo.CarId,
                Price = tripCreatingInfo.Price,
                TotalSeats = tripCreatingInfo.TotalSeats,
                TimeOfDeparture = tripCreatingInfo.TimeOfDeparture,
                TakenSeats = 0,
                Description = tripCreatingInfo.Description,
                IsDeleted = false,
                IsReported = false,
                IsFinished = false,
                DateCreated = DateTime.UtcNow,
            };

            mockedMappingProvider.Setup(x => x.Map<TripCreationInfo, Trip>(tripCreatingInfo))
                .Returns(trip);

            // Act
            tripService.CreateTrip(tripCreatingInfo);
            var createdTripUserTrips = trip.TripsUsers.FirstOrDefault(x => x.Trip == trip);

            // Assert
            Assert.AreEqual(createdTripUserTrips.UserTripStatusId, (int)UserTripStatusType.Owner);
            Assert.AreEqual(createdTripUserTrips.UserId, tripCreatingInfo.DriverId);
            Assert.AreEqual(createdTripUserTrips.Trip, trip);
            Assert.AreEqual(tripCreatingInfo.CarId, trip.CarId);
            Assert.AreEqual(tripCreatingInfo.Price, trip.Price);
            Assert.AreEqual(0, trip.TakenSeats);
            Assert.AreEqual(tripCreatingInfo.TotalSeats, trip.TotalSeats);
            Assert.AreEqual(tripCreatingInfo.TimeOfDeparture, trip.TimeOfDeparture);
            Assert.AreEqual(dateCreated, trip.DateCreated);
            Assert.IsFalse(trip.IsDeleted);
            Assert.IsFalse(trip.IsFinished);
            Assert.IsFalse(trip.IsReported);

            mockedTripRepo.Verify(x => x.Add(trip), Times.Once);
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
