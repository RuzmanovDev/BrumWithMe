using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Tests.CarServiceTests
{
    [TestFixture]
    public class Constructo_Should
    {
        [Test]
        public void Throw_ArgumentNullException_WithMessageContainingCarRepo_WhenCarRepoIsNull()
        {

        }

        public void DoesNotThrow_WhenAllArgumentsAreValid()
        {
            var mockedCarsRepo = new Mock<IProjectableRepositoryEf<Car>>();
            var mockedUOW = new Mock<IUnitOfWorkEF>();
        }
    }
}
