using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.CarServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessageContainingCarRepo_WhenCarRepoIsNull()
        {
            // Arange
            var carRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            // Act & Assert
            Assert.That(() => new CarService(null, () => mockedUOW.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(carRepo)));
        }

        [Test]
        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            // Arange
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();

            // Act & Assert
            Assert.DoesNotThrow(() => new CarService(mockedCarsRepo.Object, () => mockedUOW.Object));
        }
    }
}
