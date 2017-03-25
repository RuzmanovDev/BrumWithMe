using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.ReportServiceTests
{
    [TestFixture]
    public class GetReportedTrips_Should
    {
        [Test]
        public void RetrunAllReportedTrips_ThatAreNotMarkedAsDeleted()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();

            var reportService = new ReportService(mockedTripRepo.Object, () => mockedUnitOfWork.Object);

            var data = new List<Trip>()
            {
                new Trip() {IsDeleted = true, IsReported = true },
                new Trip() {IsDeleted = false, IsReported = true },
                new Trip() {IsDeleted = false, IsReported = true },
            };

            IEnumerable<TripBasicInfo> expected = null;

            mockedTripRepo.Setup(x => x.GetAllMapped<TripBasicInfo>(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns((Expression<Func<Trip, bool>> predicate) =>
                {
                    expected = data.Where(predicate.Compile()).Select(x => new TripBasicInfo()
                    {
                    });

                    return expected;
                });

            var expectedCount = data.Where(x=> !x.IsDeleted && x.IsReported).Count();

            // Act
            var result = reportService.GetReportedTrips();

            // Assert
            Assert.AreEqual(expectedCount, result.Count());
        }
    }
}
