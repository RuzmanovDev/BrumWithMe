using System;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
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
    public class RequestToJoinTrip_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WithMessageContaining_OwnerCannotJoin_WhenOwnerOfATripTriesToJoinTheTrip()
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

            var tripId = 1;
            var userId = "userId";

            UsersTrips IsUserOwner = new UsersTrips();
            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips,bool>>>()))
                 .Returns(IsUserOwner);

            Assert.That(() => tripService.RequestToJoinTrip(tripId, userId),
                Throws.InvalidOperationException.With.Message.Contain("owner"));

        }

        [Test]
        public void AddNewTripUser_WithStatusOfPending_WhenUserRequestToJoin()
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

            var tripId = 1;
            var userId = "userId";

            UsersTrips IsUserOwner = null;
            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                 .Returns(IsUserOwner);

            UsersTrips trip = null;
            mockedUserTripRepo.Setup(x => x.Add(It.IsAny<UsersTrips>()))
                  .Callback((UsersTrips createdTrip) => trip = createdTrip);

            var expectedStatusId = (int)UserTripStatusType.Pending;

            // Act
            tripService.RequestToJoinTrip(tripId, userId);

            // Assert
            Assert.AreEqual(tripId, trip.TripId);
            Assert.AreEqual(userId, trip.UserId);
            Assert.AreEqual(expectedStatusId, trip.UserTripStatusId);
        }
    }
}
