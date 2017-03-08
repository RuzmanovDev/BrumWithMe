using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Services.Providers.TimeProviders;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.TripServiceTests
{
    [TestFixture]
    public class CreateTripShould
    {
        [Test]
        public void Test()
        {
            //// Arrange
            //var mockedTripRepo = new Mock<IRepository<Trip>>();
            //var mockedCityService = new Mock<ICityService>();
            //var mockedTackedService = new Mock<ITagService>();
            //var mockedDateTimePorvider = new Mock<IDateTimeProvider>();
            //var mockedUnitOfWork = new Mock<IUnitOfWork>();
            //var mockedMapperProvider = new Mock<IMappingProvider>();

            //var tripService = new TripService(
            //    () => mockedUnitOfWork.Object,
            //    mockedCityService.Object,
            //    mockedMapperProvider.Object,
            //    mockedTackedService.Object,
            //    mockedTripRepo.Object,
            //    mockedDateTimePorvider.Object);

            //var info = new TripCreationInfo();

            //tripService.CreateTrip(info);


        }

    }
}
