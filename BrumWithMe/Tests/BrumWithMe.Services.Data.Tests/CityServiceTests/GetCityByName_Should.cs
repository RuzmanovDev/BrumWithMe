using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.CityServiceTests
{
    [TestFixture]
    public class GetCityByName_Should
    {
        [Test]
        public void ReturnNull_WhenCityNameIsNull()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            City city = null;
            mockedCityRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<City, bool>>>())).Returns(city);

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act
            var result = cityService.GetCityByName(null);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnNull_WhenCityIsMarkedAsDeleted()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            string cityName = "City";

            City city = new City() { Id = 1, Name = cityName, IsDeleted = true };
            var data = new List<City>() { city };

            City expected = null;
            mockedCityRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<City, bool>>>()))
                .Returns(
                (Expression<Func<City, bool>> predicate) =>
                    expected = data.Where(predicate.Compile()).FirstOrDefault());

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act
            var result = cityService.GetCityByName(cityName);

            // Assert
            Assert.IsNull(result);
            Assert.AreSame(expected, result);
        }

        [Test]
        public void ReturnCity_InspiteOfCasing()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            string cityName = "City";

            City city = new City() { Id = 1, Name = cityName, IsDeleted = false };
            var data = new List<City>() { city };

            City expected = null;
            mockedCityRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<City, bool>>>()))
                .Returns(
                (Expression<Func<City, bool>> predicate) =>
                    expected = data.Where(predicate.Compile()).FirstOrDefault());

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act
            var result = cityService.GetCityByName("cItY");

            // Assert
            Assert.IsNotNull(result);
            Assert.False(result.IsDeleted);
            Assert.AreSame(expected, result);
        }
    }
}
