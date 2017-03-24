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
    public class GetDeletedTrips_Should
    {
        [Test]
        public void RetrunOnlyDeletedNonFinishedTrips()
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

            var data = new List<Trip>()
            {
                new Trip() {IsDeleted = true, IsFinished = false },
                new Trip() {IsDeleted = true, IsFinished = false },
                new Trip() {IsDeleted = true, IsFinished = false }
            };

            IEnumerable<TripBasicInfo> expected = null;
            mockedTripRepo.Setup(x => x.GetAllMapped<TripBasicInfo>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns((Expression<Func<Trip, bool>> predicate) =>
                {
                    expected = data.Where(predicate.Compile()).Select(x => new TripBasicInfo());
                    return expected;
                });

            // Act
            var result = tripService.GetDeletedTrips();

            // Assert
            Assert.AreEqual(data.Count, result.Count());
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReturnEmptyCollction_WhenDeletedTripsAreMarkedAsFinished()
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

            var data = new List<Trip>()
            {
                new Trip() {IsDeleted = true, IsFinished = true },
                new Trip() {IsDeleted = true, IsFinished = true },
                new Trip() {IsDeleted = true, IsFinished = true }
            };

            IEnumerable<TripBasicInfo> expected = null;
            mockedTripRepo.Setup(x => x.GetAllMapped<TripBasicInfo>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns((Expression<Func<Trip, bool>> predicate) =>
                {
                    expected = data.Where(predicate.Compile()).Select(x => new TripBasicInfo());
                    return expected;
                });

            // Act
            var result = tripService.GetDeletedTrips();

            // Assert
            Assert.AreEqual(0, result.Count());
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
