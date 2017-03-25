using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    class Index_Should
    {
        [Test]
        public void Render_DefaultView()
        {
            // Arrange
            var mockedAccountManagementService = new Mock<IAccountManagementService>();
            var controller = new HomeController(mockedAccountManagementService.Object);

            // Act and Assert
            controller.WithCallTo(c => c.Index(0))
               .ShouldRenderDefaultView();
        }
    }
}
