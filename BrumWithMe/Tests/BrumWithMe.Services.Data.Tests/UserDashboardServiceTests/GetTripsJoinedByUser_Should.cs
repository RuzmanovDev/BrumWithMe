using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.Enums;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.UserDashboardServiceTests
{
    [TestFixture]
    public class GetTripsJoinedByUser_Should
    {
        [Test]
        public void ReturnThreeItems_WhenUsersIsInThreeTrips()
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

            var userId = "UserId";

            var trip = new Trip() { Id = 1, IsDeleted = false, IsFinished = false };
            var trip1 = new Trip() { Id = 2, IsDeleted = false, IsFinished = false };

            var trip2 = new Trip() { Id = 3, IsDeleted = true, IsFinished = false };
            var trip3 = new Trip() { Id = 4, IsDeleted = false, IsFinished = true };

            var data = new List<UsersTrips>()
            {
                new UsersTrips() { TripId= 1, Trip = trip, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Accepted },
                new UsersTrips() { TripId= 2, Trip = trip1, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Pending },
                new UsersTrips() { TripId= 3, Trip = trip2, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Pending },
                new UsersTrips() { TripId= 4, Trip = trip3, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Owner },
            };

            mockedUserTripRepo.Setup(x => x.GetAllMapped<TripBasicInfoWithStatus>(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                .Returns((Expression<Func<UsersTrips, bool>> predicate) =>
                {
                    return data.Where(predicate.Compile()).Select(x => new TripBasicInfoWithStatus()
                    {
                        Id = x.TripId
                    });
                });

            var expectedCount = 3;

            // Act
            var result = service.GetTripsJoinedByUser(userId);

            // Assert
            Assert.AreEqual(expectedCount, result.Count());
        }

        [Test]
        public void ReturnEmpyCollection_WhenUserIsNotInAnyTrip()
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

            var userId = "UserId";

            var trip = new Trip() { Id = 1, IsDeleted = false, IsFinished = false };
            var trip1 = new Trip() { Id = 2, IsDeleted = false, IsFinished = false };

            var trip2 = new Trip() { Id = 3, IsDeleted = true, IsFinished = false };
            var trip3 = new Trip() { Id = 4, IsDeleted = false, IsFinished = true };

            var data = new List<UsersTrips>()
            {
                new UsersTrips() { TripId= 1, Trip = trip, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Owner },
                new UsersTrips() { TripId= 2, Trip = trip1, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Owner },
                new UsersTrips() { TripId= 3, Trip = trip2, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Owner },
                new UsersTrips() { TripId= 4, Trip = trip3, UserId = userId, UserTripStatusId = (int) UserTripStatusType.Owner },
            };

            mockedUserTripRepo.Setup(x => x.GetAllMapped<TripBasicInfoWithStatus>(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                .Returns((Expression<Func<UsersTrips, bool>> predicate) =>
                {
                    return data.Where(predicate.Compile()).Select(x => new TripBasicInfoWithStatus()
                    {
                        Id = x.TripId
                    });
                });

            var expectedCount = 0;

            // Act
            var result = service.GetTripsJoinedByUser(userId);

            // Assert
            Assert.AreEqual(expectedCount, result.Count());
        }
    }
}
