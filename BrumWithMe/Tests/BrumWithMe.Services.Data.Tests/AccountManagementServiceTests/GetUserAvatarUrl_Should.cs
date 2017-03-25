using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.AccountManagementServiceTests
{
    [TestFixture]
    class GetUserAvatarUrl_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_LoggedUser_WhenLoggedUserIsNUll()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            // Act and Assert
            string loggedUser = null;
            Assert.That(() => service.GetUserAvatarUrl(loggedUser),
                Throws.ArgumentNullException.With.Message.Contain(nameof(loggedUser)));
        }

        [Test]
        public void Throw_ArgumentExcpetion_WithMessageContaining_LoggedUser_WhenLoggedUserIsEmptyString()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            // Act and Assert
            string loggedUser = string.Empty;
            Assert.That(() => service.GetUserAvatarUrl(loggedUser),
                Throws.ArgumentException.With.Message.Contain(nameof(loggedUser)));
        }

        [Test]
        public void RetunNull_WhenThereIsNoFoundUser()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            string loggedUserId = "loggedUserId";
            User user = null;
            mockedUserRepo
                .Setup(x => x.GetById(loggedUserId))
                .Returns(user);

            // Act
            var result = service.GetUserAvatarUrl(loggedUserId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void RetulUserAvatar()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            string loggedUserId = "loggedUserId";
            string avatarUrl = "avatarUrl";

            User user = new User() { Id = loggedUserId, AvataImageurl = avatarUrl };

            mockedUserRepo
                .Setup(x => x.GetById(loggedUserId))
                .Returns(user);

            // Act
            var result = service.GetUserAvatarUrl(loggedUserId);

            // Assert
            Assert.AreEqual(avatarUrl,result);
        }
    }
}
