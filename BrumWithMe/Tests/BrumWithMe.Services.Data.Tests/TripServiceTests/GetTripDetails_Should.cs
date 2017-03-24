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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class GetTripDetails_Should
    {
        [Test]
        public void ReturnTripDetails_FromTheRepo()
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

            TripDetails expected = new TripDetails() { Id = 1 };

            var data = new List<Trip>()
            {
                new Trip() { Id=1 }
            };

            mockedTripRepo.Setup(x => x.GetFirstMapped<TripDetails>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns((Expression<Func<Trip, bool>> predicate) =>
                {
                    return data.Where(predicate.Compile())
                    .Select(x => expected)
                    .FirstOrDefault();
                });

            // Act
            var result = tripService.GetTripDetails(1);

            // Assert
            Assert.AreSame(expected, result);
        }
    }
}
