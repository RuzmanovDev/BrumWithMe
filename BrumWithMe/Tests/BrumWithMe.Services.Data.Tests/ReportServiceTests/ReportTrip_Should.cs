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
    public class ReportTrip_Should
    {
        [Test]
        public void MarkTripAsReported_WhenTripIsFound()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();

            var reportService =  new ReportService(mockedTripRepo.Object, () => mockedUnitOfWork.Object);

            int tripId = 1;
            var trip = new Trip() { Id = tripId };
            mockedTripRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Trip, bool>>>()))
                .Returns(trip);

            // Act
            reportService.ReportTrip(tripId);

            // Assert
            Assert.IsTrue(trip.IsReported);
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

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
            reportService.ReportTrip(tripId);

            // Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Never);
        }
    }
}
