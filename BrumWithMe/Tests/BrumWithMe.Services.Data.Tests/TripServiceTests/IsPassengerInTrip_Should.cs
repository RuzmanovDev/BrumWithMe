using System;
using System.Collections.Generic;
using System.Linq;
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
    public class IsPassengerInTrip_Should
    {
        [Test]
        public void ReturnFalse_WhenUserIsTheDriver()
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

            string passangerId = "passanger";
            int tripId = 1;
            var userTrips = new UsersTrips()
            {
                TripId = tripId,
                UserId = passangerId,
                UserTripStatusId = (int)UserTripStatusType.Owner
            };

            var data = new List<UsersTrips>() { userTrips };

            UsersTrips exp = null;
            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                .Returns((Expression<Func<UsersTrips, bool>> predicate) =>
                {
                    exp = data.Where(predicate.Compile()).FirstOrDefault();

                    return exp;
                });

            // Act 
            var result = tripService.IsPassengerInTrip(passangerId, tripId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ReturnTrue_WhenUserIsPassangerInTheTrip()
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

            string passangerId = "passanger";
            int tripId = 1;
            var userTrips = new UsersTrips()
            {
                TripId = tripId,
                UserId = passangerId,
                UserTripStatusId = (int)UserTripStatusType.Accepted
            };

            var data = new List<UsersTrips>() { userTrips };

            UsersTrips exp = null;
            mockedUserTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
                .Returns((Expression<Func<UsersTrips, bool>> predicate) =>
                {
                    exp = data.Where(predicate.Compile()).FirstOrDefault();

                    return exp;
                });

            // Act 
            var result = tripService.IsPassengerInTrip(passangerId, tripId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
