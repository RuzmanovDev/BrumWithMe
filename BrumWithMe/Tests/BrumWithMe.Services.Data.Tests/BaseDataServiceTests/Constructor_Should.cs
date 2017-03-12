using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Services.Data.Tests.BaseDataServiceTests.Mocks;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.BaseDataServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WithMessageContainsUnitOfWork_WhenUnitOfWorkIsNull()
        {
            // Arrange

            //Act & Assert
            Assert.That(() => new DerivedDataService(null),
                Throws.ArgumentNullException.With.Message.Contain(nameof(Func<IUnitOfWorkEF>)));
        }


        [Test]
        public void NotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var unitOfWork = new Mock<Func<IUnitOfWorkEF>>();

            //Act & Assert
            Assert.DoesNotThrow(() => new DerivedDataService(unitOfWork.Object));
        }

        [Test]
        public void AssignCorrectParams()
        {
            // Arrange
            var unitOfWork = new Mock<Func<IUnitOfWorkEF>>();

            //Act
            var service = new DerivedDataService(unitOfWork.Object);

            //Assert
            Assert.AreSame(unitOfWork.Object, service.GetUnitOfWork);
        }
    }
}
