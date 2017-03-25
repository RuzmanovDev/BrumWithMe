using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.AccountManagementServiceTests
{
    [TestFixture]
    class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithMessageContaining_CarsRepo_WhenCarsRepoIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            IProjectableRepositoryEf<Car> carsRepo = null;
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            // Act and Assert
            Assert.That(() => new AccountManagementService(null, mockedUserRepo.Object, mockedUnitOfWork.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(carsRepo)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContaining_UsersRepo_WhenUsersRepoIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            IProjectableRepositoryEf<User> userRepo = null;

            // Act and Assert
            Assert.That(() => new AccountManagementService(mockedCarsRepo.Object, null, mockedUnitOfWork.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(userRepo)));
        }

        [Test]
        public void ReturnAndInstance_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            // Act
            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            // Assert
            Assert.IsAssignableFrom<AccountManagementService>(service);
        }
    }
}
