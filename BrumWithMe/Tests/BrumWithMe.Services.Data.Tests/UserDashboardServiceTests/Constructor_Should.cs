using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.UserDashboardServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ReturnAnInstance_AndNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedTripService = new Mock<ITripService>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.DoesNotThrow(() =>
            {
                var service = new UserDashboardService(
                    () => mockedUnitOfWork.Object,
                    mockedUserTripRepo.Object,
                    mockedTripService.Object,
                    mockedTripRepo.Object);
            });
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingTriprepo_WhenTripRepoIsNull()
        {

            // Arrange
            IProjectableRepositoryEf<Trip> tripRepo = null;
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            var mockedTripService = new Mock<ITripService>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() =>
            {
                var service = new UserDashboardService(
                    () => mockedUnitOfWork.Object,
                    mockedUserTripRepo.Object,
                    mockedTripService.Object,
                    tripRepo);
            },
            Throws.ArgumentNullException.With.Message.Contain(nameof(tripRepo))
            );
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingUsersTripRepo_WhenUsersTripRepoIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            IProjectableRepositoryEf<UsersTrips> userTripsRepo = null;
            var mockedTripService = new Mock<ITripService>();
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() =>
            {
                var service = new UserDashboardService(
                    () => mockedUnitOfWork.Object,
                    userTripsRepo,
                    mockedTripService.Object,
                    mockedTripRepo.Object);
            },
            Throws.ArgumentNullException.With.Message.Contain(nameof(userTripsRepo))
            );
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingTripService_WhenTripServiceIsNull()
        {
            // Arrange
            var mockedTripRepo = new Mock<IProjectableRepositoryEf<Trip>>();
            var mockedUserTripRepo = new Mock<IProjectableRepositoryEf<UsersTrips>>();
            ITripService tripService = null;
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();

            // Act and Assert
            Assert.That(() =>
            {
                var service = new UserDashboardService(
                    () => mockedUnitOfWork.Object,
                    mockedUserTripRepo.Object,
                    tripService,
                    mockedTripRepo.Object);
            },
            Throws.ArgumentNullException.With.Message.Contain(nameof(tripService))
            );
        }
    }
}
