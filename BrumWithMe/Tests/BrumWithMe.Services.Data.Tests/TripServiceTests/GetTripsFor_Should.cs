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
    public class GetTripsFor_Should
    {
        [Test]
        public void ReturnTrips_FromProvidedOrigin_ToProvidedDestination_InspiteOfCasing()
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

            string originName = "Sofia";
            string destinationName = "Plovdiv";

            var origin = new City() { Name = originName };
            var destination = new City() { Name = destinationName };

            var trip1 = new Trip() { Origin = origin, Destination = destination, DateCreated = DateTime.Now };

            var data = new List<Trip>()
            {
               trip1
            };

            IEnumerable<int> allTrisIds = null;
            mockedTripRepo.Setup(x => x.GetAll(
                It.IsAny<Expression<Func<Trip, bool>>>(),
                It.IsAny<Expression<Func<Trip, int>>>()))
                .Returns((Expression<Func<Trip, bool>> predicate,
                Expression<Func<Trip, int>> select) =>
                {
                    allTrisIds = data.Where(predicate.Compile()).Select(select.Compile());
                    return allTrisIds;
                });


            int page = 0;
            int size = 5;

            IEnumerable<TripBasicInfo> paggedTrips = null;
            mockedTripRepo.Setup(x =>
            x.GetAllMapped<DateTime, TripBasicInfo>(
                It.IsAny<Expression<Func<Trip, bool>>>(),
                It.IsAny<Expression<Func<Trip, DateTime>>>(),
                page, size))
                .Returns((
                    Expression<Func<Trip, bool>> predicate,
                    Expression<Func<Trip, DateTime>> sort,
                    int pageNum,
                    int sizeToTake) =>
                {
                    paggedTrips = data
                        .Where(predicate.Compile())
                        .OrderBy(sort.Compile())
                        .Skip(pageNum * sizeToTake)
                        .Take(sizeToTake)
                        .Select(x => new TripBasicInfo()
                        {
                            Id = x.Id,
                            DestinationName = x.Destination.Name,
                            OriginName = x.Origin.Name,
                            Price = x.Price,
                            TakenSeats = x.TakenSeats,
                            TimeOfDeparture = x.TimeOfDeparture,
                            TotalSeats = x.TotalSeats,
                        });

                    return paggedTrips;
                });


            // Act
            TripSearchResult result = tripService.GetTripsFor(origin.Name, destination.Name);

            int totalTripsCount = allTrisIds.Count();
            TripSearchResult expected = new TripSearchResult()
            {
                FoundTrips = paggedTrips,
                TotalTrips = totalTripsCount
            };

            // Assert
            Assert.AreEqual(expected.TotalTrips, result.TotalTrips);
            Assert.AreEqual(expected.FoundTrips.ToList()[0].Id, result.FoundTrips.ToList()[0].Id);
            Assert.AreEqual(expected.FoundTrips.ToList()[0].OriginName, result.FoundTrips.ToList()[0].OriginName);
            Assert.AreEqual(expected.FoundTrips.ToList()[0].DestinationName, result.FoundTrips.ToList()[0].DestinationName);
        }
    }
}
