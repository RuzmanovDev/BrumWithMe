using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.CarServiceTests
{
    [TestFixture]
    public class AddCarToUser_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessageContainingCar_WhenCarIsNull()
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            var service = new CarService(mockedCarsRepo.Object, () => mockedUOW.Object);
            var userId = "userId";
            Car car = null;

            // Act & Assert
            Assert.That(() => service.AddCarToUser(car, userId),
                Throws.ArgumentNullException.With.Message.Contain(nameof(car)));
        }

        [Test]
        public void Throw_ArgumentNullException_WithMessageContainingUserId_WhenUserIdNull()
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            var service = new CarService(mockedCarsRepo.Object, () => mockedUOW.Object);
            string userId = null;
            Car car = new Car();

            // Act & Assert
            Assert.That(() => service.AddCarToUser(car, userId),
                Throws.ArgumentNullException.With.Message.Contain(nameof(userId)));
        }

        [Test]
        public void Throw_ArgumentException_WithMessageContainingUserId_WhenUserIdEmpty()
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            var service = new CarService(mockedCarsRepo.Object, () => mockedUOW.Object);
            string userId = string.Empty;
            Car car = new Car();

            // Act & Assert
            Assert.That(() => service.AddCarToUser(car, userId),
                Throws.ArgumentException.With.Message.Contain(nameof(userId)));
        }

        [TestCase("use=dasd")]
        [TestCase("userId")]
        [TestCase("1231")]
        public void AssignedUserIdToTheCar_AfterCallOnReposAndUOW_FromPassedParameter(string userId)
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            var service = new CarService(mockedCarsRepo.Object, () => mockedUOW.Object);
            Car car = new Car();

            // Act
            service.AddCarToUser(car, userId);

            // Assert
            mockedCarsRepo.Verify(x => x.Add(car), Times.Once);
            mockedUOW.Verify(x => x.Commit(), Times.Once);
            Assert.AreEqual(userId, car.OwenerId);
        }
    }
}
