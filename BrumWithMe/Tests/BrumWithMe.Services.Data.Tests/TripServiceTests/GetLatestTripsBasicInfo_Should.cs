//using BrumWithMe.Data.Contracts;
//using BrumWithMe.Data.Models.CompositeModels.Trip;
//using BrumWithMe.Data.Models.Entities;
//using BrumWithMe.Services.Data.Contracts;
//using BrumWithMe.Services.Data.Services;
//using BrumWithMe.Services.Providers.Mapping.Contracts;
//using BrumWithMe.Services.Providers.TimeProviders;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;

//namespace BrumWithMe.Services.Data.Tests.TripServiceTests
//{
//    [TestFixture]
//    public class GetLatestTripsBasicInfo_Should
//    {
//        [Test]
//        public void ReturnLatestTrips_FromTheRepo()
//        {
//            // Arrange
//            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
//            var mockedCityService = new Mock<ICityService>();
//            var mockedTackedService = new Mock<ITagService>();
//            var mockedDateTimePorvider = new Mock<IDateTimeProvider>();
//            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
//            var mockedMapperProvider = new Mock<IMappingProvider>();

//            var tripService = new TripService(
//                () => mockedUnitOfWork.Object,
//                mockedCityService.Object,
//                mockedMapperProvider.Object,
//                mockedTackedService.Object,
//                mockedTripRepo.Object,
//                mockedDateTimePorvider.Object);

//            var expected = new List<TripBasicInfo>();
//            var data = new List<Trip>();

//            mockedTripRepo.Setup(x => x.GetAllMapped<DateTime, TripBasicInfo>(It.IsAny<Expression<Func<Trip, bool>>>(),
//                It.IsAny<Expression<Func<Trip, DateTime>>>(), It.IsAny<int>(), It.IsAny<int>()))
//                .Returns(expected);

//            // Act
//            var result = tripService.GetLatestTripsBasicInfo(It.IsAny<int>());

//            // Assert
//            CollectionAssert.AreEquivalent(expected, result);
//        }
//    }
//}
