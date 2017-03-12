using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.TagServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessage_Contains_TagRepo_WhenTagRepoIsNull()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            IProjectableRepositoryEf<Tag> tagRepo = null;

            // Act & Assert 
            Assert.That(() => new TagService(null, () => mockedUOW.Object),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tagRepo)));
        }

        [Test]
        public void NotThrow_WhenAllArgumentsAreValid()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var tagRepo = new Mock<IProjectableRepositoryEf<Tag>>();

            // Act & Assert 
            Assert.DoesNotThrow(() => new TagService(tagRepo.Object, () => mockedUOW.Object));
        }
    }
}
