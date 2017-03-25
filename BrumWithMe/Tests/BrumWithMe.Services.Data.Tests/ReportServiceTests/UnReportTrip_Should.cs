using System;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.ReportServiceTests
{
    [TestFixture]
    public class UnReportTrip_Should
    {
        [Test]
        public void NotcallCommit_WhenFoundTripisNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();

            var reportService = new ReportService(mockedTripRepo.Object, () => mockedUnitOfWork.Object);

            int tripId = 1;
            Trip trip = null;
            mockedTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(trip);

            // Act
            reportService.UnReportTrip(tripId);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Never);
        }

        [Test]
        public void UnreportTrip_WhenArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();

            var reportService = new ReportService(mockedTripRepo.Object, () => mockedUnitOfWork.Object);

            int tripId = 1;
            var trip = new Trip() { Id = tripId , IsReported = true };
            mockedTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(trip);

            // Act
            reportService.UnReportTrip(tripId);

            // Assert
            Assert.IsFalse(trip.IsReported);
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
