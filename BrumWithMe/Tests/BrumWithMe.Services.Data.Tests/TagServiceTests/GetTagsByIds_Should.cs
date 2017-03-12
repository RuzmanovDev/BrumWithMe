using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.TagServiceTests
{
    [TestFixture]
    public class GetTagsByIds_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessageContaining_TagIds_WhenTagIdsIsNull()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var tagRepo = new Mock<IProjectableRepositoryEf<Tag>>();

            var tagService = new TagService(tagRepo.Object, () => mockedUOW.Object);
            IEnumerable<int> tagIds = null;

            // Act & Assert
            Assert.That(() => tagService.GetTagsByIds(tagIds),
                Throws.ArgumentNullException.With.Message.Contain(nameof(tagIds)));

        }

        [Test]
        public void ReturnEmpyCollection_WhenProvidedIdsCountIsZero()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var tagRepo = new Mock<IProjectableRepositoryEf<Tag>>();

            var tagService = new TagService(tagRepo.Object, () => mockedUOW.Object);
            IEnumerable<int> tagIds = new List<int>();

            // Act
            var result = tagService.GetTagsByIds(tagIds);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void ReturnOnlyNonMarkedAsDeletedTags_WithProvidedIds()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var tagRepo = new Mock<IProjectableRepositoryEf<Tag>>();

            IEnumerable<int> tagIds = new List<int>() { 1, 2 };
            var data = new List<Tag>()
            {
                new Tag() { Id = 1, Name = "Test", IsDeleted = false },
                new Tag() { Id = 2, Name = "Pesho", IsDeleted = true }
            };

            IEnumerable<Tag> expected = null;

            tagRepo.Setup(x => x.GetAll(It.IsAny<Expression<Func<Tag, bool>>>(), It.IsAny<Expression<Func<Tag, Tag>>>()))
                .Returns(
                (Expression<Func<Tag, bool>> predicate,
                Expression<Func<Tag, Tag>> select) =>
                expected = data.Where(predicate.Compile()).Select(select.Compile()));

            var tagService = new TagService(tagRepo.Object, () => mockedUOW.Object);

            // Act
            var result = tagService.GetTagsByIds(tagIds).ToList();

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result[0].Id);
            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
