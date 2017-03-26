using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests.Controllers.ReviewControllerTests
{
    [TestFixture]
    public class Construcor_Should
    {
        [Test]
        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            var mockedReviewService = new Mock<IReviewService>();

            // Act and Assert
            Assert.DoesNotThrow(() => new ReviewController(mockedMappingProvider.Object, mockedReviewService.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessgeContainingMappingProvider_WhenMappingProviderIsNull()
        {
            // Arrange
            IMappingProvider mappingProvider = null;
            var mockedReviewService = new Mock<IReviewService>();

            // Act and Assert
            Assert.That(() => new ReviewController(mappingProvider, mockedReviewService.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(mappingProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WithMessageContainingReviewService_WhenReviewServiceIsNull()
        {
            // Arrange
            var mockedMappingProvider = new Mock<IMappingProvider>();
            IReviewService reviewService = null;

            // Act and Assert
            Assert.That(() => new ReviewController(mockedMappingProvider.Object, reviewService),
                Throws.ArgumentNullException.With.Message.Contain(nameof(reviewService)));
        }
    }
}
