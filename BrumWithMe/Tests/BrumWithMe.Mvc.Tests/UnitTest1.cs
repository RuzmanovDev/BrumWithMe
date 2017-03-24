using BrumWithMe.MVC.Controllers;
using Moq;
using NUnit.Framework;

namespace BrumWithMe.Mvc.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var controller = new Mock<BaseController>();
        }   
    }
}
