using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.CityServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessagContaining_CityRepo_WhenCityRepoIsNull()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            IRepositoryEf<City> cityRepo = null;

            // Act & Assert
            Assert.That(() => new CityService(cityRepo, () => mockedUOW.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(cityRepo)));
        }

        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            // Act & Assert
            Assert.DoesNotThrow(() => new CityService(mockedCityRepo.Object, () => mockedUOW.Object));
        }
    }
}
