using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Services.Providers.TimeProviders;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class GetLatestTripsBasicInfo_Should
    {
        [Test]
        public void ReturnLatestTrips_FromTheRepo()
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

            var expected = new List<TripBasicInfo>();
            var data = new List<Trip>();

            mockedTripRepo.Setup(x => x.GetAllMapped<DateTime, TripBasicInfo>(It.IsAny<Expression<Func<Trip, bool>>>(),
                It.IsAny<Expression<Func<Trip, DateTime>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expected);

            // Act
            var result = tripService.GetLatestTripsBasicInfo(It.IsAny<int>());

            // Assert
            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
