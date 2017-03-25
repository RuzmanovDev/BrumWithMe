using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.AccountManagementServiceTests
{
    [TestFixture]
    public class SetUserAvatar_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithMessageContainingLoggedUserId_WhenUserIdIsNUll()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            string logedUserId = null;
            string imageUrl = "ImageUrl";

            // Act and Assert
            Assert.That(() => service.SetUserAvatar(logedUserId, imageUrl),
                Throws.ArgumentNullException.With.Message.Contain(nameof(logedUserId)));
        }

        [Test]
        public void ThrowArgument_WithMessageContainingLoggedUserId_WhenUserIdIsEmpty()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            string logedUserId = string.Empty;
            string imageUrl = "ImageUrl";

            // Act and Assert
            Assert.That(() => service.SetUserAvatar(logedUserId, imageUrl),
                Throws.ArgumentException.With.Message.Contain(nameof(logedUserId)));
        }

        [Test]
        public void ThrowArgumentNullException_WitMessageContainingImagaeUrl_WhenImageUrlIsNull()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            string logedUserId = "userId";
            string imageUrl = null;

            // Act and Assert
            Assert.That(() => service.SetUserAvatar(logedUserId, imageUrl),
                Throws.ArgumentNullException.With.Message.Contain(nameof(imageUrl)));
        }

        [Test]
        public void ThrowArgumentException_WithMessageContatiingImageUrl_WhenImageUrlIsEmpty()
        {

            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            string logedUserId = "userId";
            string imageUrl = string.Empty;

            // Act and Assert
            Assert.That(() => service.SetUserAvatar(logedUserId, imageUrl),
                Throws.ArgumentException.With.Message.Contain(nameof(imageUrl)));
        }

        [Test]
        public void SetUserAvatarCorectly()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWorkEF>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, () => mockedUnitOfWork.Object);

            string logedUserId = "userId";
            string imageUrl = "imageUrl";

            var user = new User() { Id = logedUserId };
            mockedUserRepo.Setup(x => x.GetById(logedUserId))
                .Returns(user);

            // Act
            service.SetUserAvatar(logedUserId, imageUrl);

            // Assert
            Assert.AreEqual(imageUrl, user.AvataImageurl);
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
