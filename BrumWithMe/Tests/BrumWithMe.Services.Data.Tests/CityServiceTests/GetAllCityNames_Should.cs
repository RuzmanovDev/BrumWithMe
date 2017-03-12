using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.CityServiceTests
{
    [TestFixture]
    public class GetAllCityNames_Should
    {
        [Test]
        public void ReturnNonMarkedAsDeletedCityNames()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            var cityName = "test";
            var cityName1 = "test1";
            var cityName2 = "test2";

            var city = new City() { Name = cityName, IsDeleted = false };
            var city1 = new City() { Name = cityName1, IsDeleted = false };
            var city2 = new City() { Name = cityName2, IsDeleted = false };

            var data = new List<City>() { city, city1, city2 };

            IEnumerable<string> cityNames = new List<string>() { cityName, cityName1, cityName2 }; ;

            mockedCityRepo.Setup(
                x => x.GetAll(It.IsAny<Expression<Func<City, bool>>>(), It.IsAny<Expression<Func<City, string>>>()))
            .Returns((Expression<Func<City, bool>> predicate,
            Expression<Func<City, string>> select) =>
            data.Where(predicate.Compile()).Select(select.Compile()));

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act
            var result = cityService.GetAllCityNames();

            // Assert
            CollectionAssert.AreEquivalent(cityNames, result);
        }

        [Test]
        public void ReturnEmptyCollection_WhenAllCitiesAreMarkedAsDeleted()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var mockedCityRepo = new Mock<IRepositoryEf<City>>();

            var cityName = "test";
            var cityName1 = "test1";
            var cityName2 = "test2";

            var city = new City() { Name = cityName, IsDeleted = true };
            var city1 = new City() { Name = cityName1, IsDeleted = true };
            var city2 = new City() { Name = cityName2, IsDeleted = true };

            var data = new List<City>() { city, city1, city2 };

            IEnumerable<string> cityNames = new List<string>() { cityName, cityName1, cityName2 }; ;

            mockedCityRepo.Setup(
                x => x.GetAll(It.IsAny<Expression<Func<City, bool>>>(), It.IsAny<Expression<Func<City, string>>>()))
            .Returns((Expression<Func<City, bool>> predicate,
            Expression<Func<City, string>> select) =>
            data.Where(predicate.Compile()).Select(select.Compile()));

            var cityService = new CityService(mockedCityRepo.Object, () => mockedUOW.Object);

            // Act
            var result = cityService.GetAllCityNames();

            // Assert
            CollectionAssert.AreNotEqual(cityNames, result);
            Assert.AreEqual(0, result.Count());
        }
    }
}
