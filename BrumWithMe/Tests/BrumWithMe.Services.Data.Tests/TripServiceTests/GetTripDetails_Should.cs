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
            var mockedCityService = new Mock<ICityService>();
            var mockedTackedService = new Mock<ITagService>();
            var mockedDateTimePorvider = new Mock<IDateTimeProvider>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedMapperProvider = new Mock<IMappingProvider>();

            var tripService = new TripService(
                () => mockedUnitOfWork.Object,
                mockedCityService.Object,
                mockedMapperProvider.Object,
                mockedTackedService.Object,
                mockedTripRepo.Object,
                mockedDateTimePorvider.Object);

            TripDetails expected = new TripDetails();

            mockedTripRepo.Setup(x => x.GetFirstMapped<TripDetails>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(expected);

            // Act
            var result = tripService.GetTripDetails(1);

            // Assert
            Assert.AreSame(expected, result);
        }
    }
}
