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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class GetPassengersForTheTrip_Should
    {
        [Test]
        public void ReturnOnlyPassangers_ForTheTripWithProviderId()
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

            var data = new List<UsersTrips>()
            {
                new UsersTrips() {TripId = 1, UserTripStatusId = (int)UserTripStatusType.Pending },
                new UsersTrips() {TripId = 1, UserTripStatusId = (int)UserTripStatusType.Accepted },
                new UsersTrips() {TripId = 1, UserTripStatusId = (int)UserTripStatusType.Owner },
            };
            int countOfPassangersInTheTrip = 2;

            List<PassangerInfo> expectedPassangers = null;
            mockedUserTripRepo.Setup(x => x.GetAllMapped<PassangerInfo>(It.IsAny<Expression<Func<UsersTrips, bool>>>()))
            .Returns((Expression<Func<UsersTrips, bool>> predicate) =>
            {
                expectedPassangers = data.Where(predicate.Compile()).Select(x => new PassangerInfo()
                {
                    TripId = x.TripId
                }).ToList();

                return expectedPassangers;
            });

            // Act
            var result = tripService.GetPassengersForTheTrip(1).ToList();

            // Assert
            Assert.AreEqual(countOfPassangersInTheTrip, result.Count());
            Assert.AreEqual(expectedPassangers[0].Id, result[0].Id);
            Assert.AreEqual(expectedPassangers[1].Id, result[1].Id);

        }
    }
}
