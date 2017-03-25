using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.ReportServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithMessageContaining_TripRepo_WhenTripRepoIsNull()
        {
            // Arrange
            var mokcedUnitOfWork = new Moq.Mock<IUnitOfWorkEF>();
            IProjectableRepositoryEf<Trip> tripRepo = null;

            // Act and Assert
            Assert.That(() => new ReportService(tripRepo, () => mokcedUnitOfWork.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tripRepo)));
        }
        
        [Test]
        public void ReturnAnInstance_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mokcedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();

            // Act and Assert
            Assert.DoesNotThrow(() => new ReportService(mockedTripRepo.Object, () => mokcedUnitOfWork.Object));
        }
    }
}
