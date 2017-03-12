using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.CarServiceTests
{
    [TestFixture]
    public class GetUserCars_Should
    {
        [Test]
        public void Throw_ArgumentException_WithMessageContainingUserId_WhenUserIdIsEmpty()
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            var service = new CarService(mockedCarsRepo.Object, () => mockedUOW.Object);
            string userId = null;

            // Act & Assert
            Assert.That(() => service.GetUserCars(userId),
                Throws.ArgumentNullException.With.Message.Contain(nameof(userId)));
        }


        [Test]
        public void Throw_ArgumentNullException_WithMessageContainingUserId_WhenUserIdIsNull()
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            var service = new CarService(mockedCarsRepo.Object, () => mockedUOW.Object);
            string userId = string.Empty;

            // Act & Assert
            Assert.That(() => service.GetUserCars(userId),
                Throws.ArgumentException.With.Message.Contain(nameof(userId)));
        }
    }
}
