using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using BrumWithMe.Data.Models.CompositeModels.Trip;

namespace BrumWithMe.Services.Data.Tests.UserDashboardServiceTests
{
    [TestFixture]
    public class JoinUserToTrip_Should
    {
        [Test]
        public void ThrowInvalidOperationException_WhenUserIsAOwner()
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

            var userId = "userId";
            int tripId = 1;

            mockedTripService.Setup(x => x.IsUserOwnerOfTrip(userId, tripId)).Returns(true);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => service.JoinUserToTrip(userId, tripId));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenTakenSeatsBecomeGreaterThanTheTotalSeats()
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

            var userId = "userId";
            int tripId = 1;

            var trip = new Trip() { Id = tripId, TotalSeats = 4, TakenSeats = 4 };

            mockedTripService.Setup(x => x.IsUserOwnerOfTrip(userId, tripId)).Returns(false);
            mockedTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(trip);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => service.JoinUserToTrip(userId, tripId));
        }

        [Test]
        public void IncrementTakenSeats()
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

            var userId = "userId";
            int tripId = 1;

            var trip = new Trip() { Id = tripId, TotalSeats = 4, TakenSeats = 3 };

            mockedTripService.Setup(x => x.IsUserOwnerOfTrip(userId, tripId)).Returns(false);
            mockedTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(trip);

            var expectedSeats = trip.TakenSeats + 1;
            // Act
            service.JoinUserToTrip(userId, tripId);

            //Assert
            Assert.AreEqual(expectedSeats, trip.TakenSeats);
        }

        [Test]
        public void ReturnTheTrip_WhenJoiningToItWasSuccessfull()
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

            var userId = "userId";
            int tripId = 1;

            var trip = new Trip() { Id = tripId, TotalSeats = 4, TakenSeats = 3 };

            mockedTripService.Setup(x => x.IsUserOwnerOfTrip(userId, tripId))
                .Returns(false);

            mockedTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(trip);

            var expected = new TripInfoWithUserRequests();
            mockedTripRepo.Setup(x => x.GetFirstMapped<TripInfoWithUserRequests>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(expected);

            // Act
            var result = service.JoinUserToTrip(userId, tripId);

            //Assert
            Assert.AreSame(expected, result);
        }
    }
}
