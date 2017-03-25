using System;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.UserDashboardServiceTests
{
    [TestFixture]
    public class RejectUserToJoinTrip_Should
    {
        [Test]
        public void CallSignOut_FroMTripService_WithCorrectParamethers()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedTripService = new Mock<ITripService>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new UserDashboardService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                mockedTripService.Object,
                mockedTripRepo.Object);

            var userId = "userid";
            var tripId = 1;

            mockedTripService.Setup(x => x.SignOutOfTrip(tripId, userId));

            // Act
            service.RejectUserToJoinTrip(userId, tripId);

            // Assert
            mockedTripService.Verify(x => x.SignOutOfTrip(tripId, userId), Times.Once);
        }

        [Test]
        public void ReturnUpdatedTripInfo()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedTripService = new Mock<ITripService>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            var service = new UserDashboardService(
                () => mockedUnitOfWork.Object,
                mockedUserTripRepo.Object,
                mockedTripService.Object,
                mockedTripRepo.Object);

            var userId = "userid";
            var tripId = 1;

            mockedTripService.Setup(x => x.SignOutOfTrip(tripId, userId));

            var expected = new TripInfoWithUserRequests();
            mockedTripRepo.Setup(x => x.GetFirstMapped<TripInfoWithUserRequests>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(expected);

            // Act
            var result = service.RejectUserToJoinTrip(userId, tripId);

            // Assert
            mockedTripService.Verify(x => x.SignOutOfTrip(tripId, userId), Times.Once);
            Assert.AreSame(expected, result);
        }
    }
}
