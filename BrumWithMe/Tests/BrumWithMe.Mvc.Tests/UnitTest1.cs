﻿using BrumWithMe.MVC.Controllers;
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
            var test = "test";
            var controller = new BaseController()
            {
                GetUserId = () => test
            };

            var result = controller.GetUserId;

            Assert.AreEqual(test, result());
        }
    }
}
