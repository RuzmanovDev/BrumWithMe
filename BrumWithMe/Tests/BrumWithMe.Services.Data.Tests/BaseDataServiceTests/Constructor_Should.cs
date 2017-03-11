using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Services.Data.Tests.BaseDataServiceTests.Mocks;
using BrumWithMe.Services.Providers.Mapping.Contracts;
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
            var mappingProvider = new Mock<IMappingProvider>();

            //Act & Assert
            Assert.That(() => new DerivedDataService(null, mappingProvider.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(Func<IUnitOfWorkEF>)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingMappingProvider_WhenMappingProviderIsNull()
        {
            // Arrange
            var unitOfWork = new Mock<Func<IUnitOfWorkEF>>();

            //Act & Assert
            Assert.That(() => new DerivedDataService(unitOfWork.Object, null),
                Throws.ArgumentNullException.With.Message.Contain(nameof(IMappingProvider)));
        }

        [Test]
        public void NotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var unitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mappingProvider = new Mock<IMappingProvider>();

            //Act & Assert
            Assert.DoesNotThrow(() => new DerivedDataService(unitOfWork.Object, mappingProvider.Object));
        }

        [Test]
        public void AssignCorrectParams()
        {
            // Arrange
            var unitOfWork = new Mock<Func<IUnitOfWorkEF>>();
            var mappingProvider = new Mock<IMappingProvider>();

            //Act
            var service = new DerivedDataService(unitOfWork.Object, mappingProvider.Object);

            //Assert
            Assert.AreSame(unitOfWork.Object, service.GetUnitOfWork);
            Assert.AreSame(mappingProvider.Object, service.GetMappingProvider);
        }
    }
}
