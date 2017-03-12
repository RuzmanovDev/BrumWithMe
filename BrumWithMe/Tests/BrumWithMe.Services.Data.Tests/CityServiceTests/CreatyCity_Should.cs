using System.Collections.Generic;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.CityServiceTests
{
    [TestFixture]
    public class CreatyCity_Should
    {
        [Test]
        public void Throw_ArgumentNulLException_WithMessageContaining_CityName_WhenCityNameIsNull()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);
            string cityName = null;

            // Act & Assert
            Assert.That(() => cityService.CreateCity(cityName),
                Throws.ArgumentNullException.With.Message.Contains(nameof(cityName)));
        }

        [Test]
        public void Throw_ArgumentException_WithMessageContaining_CityName_WhenCityNameIsEmpty()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);
            string cityName = string.Empty;

            // Act & Assert
            Assert.That(() => cityService.CreateCity(cityName),
                Throws.ArgumentException.With.Message.Contains(nameof(cityName)));
        }

        [TestCase("Sofia")]
        [TestCase("Pazardzhik")]
        [TestCase("Vidin")]
        public void AddCreatedCityToTheRepo_WhenAllArgumentsAreValid(string cityName)
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();
            var city = new City() { Name = cityName };

            var data = new List<City>();

            mockedUOW.Setup(x => x.Commit())
                .Callback(() => data.Add(city));

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act 
            cityService.CreateCity(cityName);

            // Assert
            mockedCityRepo.Verify(x => x.Add(It.IsAny<City>()));
            mockedUOW.Verify(x => x.Commit(), Times.Once());
            Assert.AreSame(city, data[0]);
        }

        [TestCase("Sofia")]
        [TestCase("Pazardzhik")]
        [TestCase("Vidin")]
        public void ReturnNewlyCreatedCity_WhenArgumentsAreValid(string cityName)
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();
            var city = new City() { Name = cityName };

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act 
            var result = cityService.CreateCity(cityName);

            // Assert
            mockedCityRepo.Verify(x => x.Add(It.IsAny<City>()));
            mockedUOW.Verify(x => x.Commit(), Times.Once());
            Assert.IsAssignableFrom<City>(result);
        }
    }
}
