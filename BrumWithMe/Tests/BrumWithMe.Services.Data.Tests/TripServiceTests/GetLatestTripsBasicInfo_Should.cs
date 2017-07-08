using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class GetLatestTripsBasicInfo_Should
    {
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(4)]
        public void ReturnLatestTrips_FromTheRepo(int countToTake)
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

            IEnumerable<TripBasicInfo> expected = new List<TripBasicInfo>()
            {
                new TripBasicInfo(),
                new TripBasicInfo(),
                new TripBasicInfo(),
                new TripBasicInfo()
            };

            expected = expected.Take(countToTake);

            var data = new List<Trip>();

            mockedTripRepo.Setup(x => x.GetAllMappedWithDescSort<DateTime, TripBasicInfo>(It.IsAny<Expression<Func<Trip, bool>>>(),
                It.IsAny<Expression<Func<Trip, DateTime>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expected);

            // Act
            var result = tripService.GetLatestTripsBasicInfo(countToTake);

            // Assert
            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
