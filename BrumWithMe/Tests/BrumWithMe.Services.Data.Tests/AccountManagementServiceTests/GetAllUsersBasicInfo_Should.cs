using System;
using System.Collections.Generic;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.AccountManagementServiceTests
{
    [TestFixture]
    public class GetAllUsersBasicInfo_Should
    {
        [Test]
        public void ReturnUserBasicInfo()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUserRepo = new Mock<IProjectableRepositoryEf<User>>();

            var service = new AccountManagementService(mockedCarsRepo.Object, mockedUserRepo.Object, mockedUnitOfWork.Object);

            var expected = new List<UserBasicInfo>();

            mockedUserRepo.Setup(x => x.GetAllMapped<UserBasicInfo>())
                .Returns(expected);

            // Act
            var result = service.GetAllUsersBasicInfo();

            // Assert
            CollectionAssert.AreEquivalent(expected, result);
        }

    }
}
