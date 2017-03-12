using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Services.Data.Tests.TagServiceTests
{
    [TestFixture]
    public class GetAllTags_Should
    {
        [Test]
        public void Return_AllNotMarkedAsDeletedTags()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var tagRepo = new Mock<IProjectableRepositoryEf<Tag>>();

            var data = new List<Tag>()
            {
                new Tag() { Id = 1, Name = "Test", IsDeleted = true },
                new Tag() { Id = 2, Name = "Pesho", IsDeleted = false }
            };

            IEnumerable<TagInfo> expected = null;

            tagRepo.Setup(x => x.GetAllMapped<TagInfo>(It.IsAny<Expression<Func<Tag, bool>>>()))
                .Returns(
                (Expression<Func<Tag, bool>> predicate) =>
                expected = data.Where(predicate.Compile())
                .Select(x => new TagInfo()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList());

            var tagService = new TagService(tagRepo.Object, () => mockedUOW.Object);

            // Act
            var result = tagService.GetAllTags().ToList();

            // Assert
            Assert.AreEqual(1, result.Count());
            CollectionAssert.AreEquivalent(expected, result);
            Assert.AreEqual(2, result[0].Id);
        }

        [Test]
        public void ReturnEmptyCollection_WhenAllTagsAreMarkedAsDeleted()
        {
            // Arrange
            var mockedUOW = new Mock<IUnitOfWorkEF>();
            var tagRepo = new Mock<IProjectableRepositoryEf<Tag>>();

            var data = new List<Tag>()
            {
                new Tag() { Id = 1, Name = "Test", IsDeleted = true },
                new Tag() { Id = 2, Name = "Pesho", IsDeleted = true }
            };

            IEnumerable<TagInfo> expected = null;

            tagRepo.Setup(x => x.GetAllMapped<TagInfo>(It.IsAny<Expression<Func<Tag, bool>>>()))
                .Returns(
                (Expression<Func<Tag, bool>> predicate) =>
                expected = data.Where(predicate.Compile())
                .Select(x => new TagInfo()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList());

            var tagService = new TagService(tagRepo.Object, () => mockedUOW.Object);

            // Act
            var result = tagService.GetAllTags();

            // Assert
            Assert.AreEqual(0, result.Count());
            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}
