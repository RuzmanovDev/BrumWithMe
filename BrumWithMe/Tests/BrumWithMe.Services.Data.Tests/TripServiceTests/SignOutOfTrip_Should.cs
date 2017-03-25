using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.Enums;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Services.Providers.TimeProviders;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class SignOutOfTrip_Should
    {
        [Test]
        public void Return_False_IfUserIsOwner()
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

            int tripId = 1;
            string driverId = "driverId";

            var userTrip = new UsersTrips()
            {
                TripId = tripId,
                UserId = driverId,
                UserTripStatusId = (int)UserTripStatusType.Owner
            };

            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                .Returns(userTrip);

            // Act
            var expected = tripService.SignOutOfTrip(tripId, driverId);

            // Assert
            Assert.False(expected);
        }

        [Test]
        public void DecrementTakenSeats_FromTrip()
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

            int tripId = 1;
            string userId = "driverId";
            Trip trip = new Trip()
            {
                Id = tripId,
                TakenSeats = 5
            };

            var userTrip = new UsersTrips()
            {
                TripId = tripId,
                Trip = trip,
                UserId = userId,
                UserTripStatusId = (int)UserTripStatusType.Accepted
            };

            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                .Returns(userTrip);

            var expectedSeats = trip.TakenSeats - 1;

            // Act
            tripService.SignOutOfTrip(tripId, userId);

            // Assert
            Assert.AreEqual(expectedSeats, trip.TakenSeats);
        }

        [Test]
        public void Throw_InvalidOperationException_WhenTryingToDecrementBeyond0()
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

            int tripId = 1;
            string userId = "driverId";
            Trip trip = new Trip()
            {
                Id = tripId,
                TakenSeats = 0
            };

            var userTrip = new UsersTrips()
            {
                TripId = tripId,
                Trip = trip,
                UserId = userId,
                UserTripStatusId = (int)UserTripStatusType.Accepted
            };

            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                    .Returns(userTrip);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => tripService.SignOutOfTrip(tripId, userId));
        }

        [Test]
        public void ReturnFalse_WhenFoundTripIsNull()
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

            int tripId = 1;
            string userId = "driverId";

            UsersTrips userTrip = null;

            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                    .Returns(userTrip);

            // Act
            var result = tripService.SignOutOfTrip(tripId, userId);

            // Assert
            mockedUserTripRepo.Verify(x => x.Delete(It.IsAny<UsersTrips>()), Times.Never);
            Assert.False(result);
        }

        [Test]
        public void CallDeleteMethodWithFoundTrip_AndReturnTrue_WhenDeletingIsSuccesfull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedCityService = new Mock<ICityService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedDateTimpeProvider = new Mock<IDateTimeProvider>();
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            mockedUnitOfWork.Setup(x => x.Commit()).Returns(true);

            var tripService = new TripService(
                  () => mockedUnitOfWork.Object,
                  mockedUserTripRepo.Object,
                  mockedCityService.Object,
                  mockedMappingProvider.Object,
                  mockedTagService.Object,
                  mockedTripRepo.Object,
                  mockedDateTimpeProvider.Object);

            int tripId = 1;
            string userId = "driverId";
            Trip trip = new Trip()
            {
                Id = tripId,
                TakenSeats = 2
            };

            var userTrip = new UsersTrips()
            {
                TripId = tripId,
                Trip = trip,
                UserId = userId,
                UserTripStatusId = (int)UserTripStatusType.Accepted
            };

            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                    .Returns(userTrip);

            // Act
            var result = tripService.SignOutOfTrip(tripId, userId);

            // Assert
            mockedUserTripRepo.Verify(x => x.Delete(userTrip), Times.Once);
            Assert.True(result);
        }
    }
}
