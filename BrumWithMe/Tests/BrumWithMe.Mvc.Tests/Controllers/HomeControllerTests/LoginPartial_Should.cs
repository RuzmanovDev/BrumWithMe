using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.HomeControllerTests
{
    [TestFixture]
    public class LoginPartial_Should
    {
        [Test]
        public void ReturnLoginPartiaL_WhenThereIsNoLoggedUser()
        {
            // Arrange
            var mockedAccountManagementService = new Mock<IAccountManagementService>();
            var controller = new HomeController(mockedAccountManagementService.Object);

            controller.GetLoggedUserId = () => null;

            // Act and Assert
            controller.WithCallTo(c => c.LoginPartial())
              .ShouldRenderPartialView("_LoginPartial");
        }

        [Test]
        public void ReturnLogginPartial_WithLoggedUserAvatarInIt_WhenThereIsLoggedUser()
        {
            // Arrange
            var mockedAccountManagementService = new Mock<IAccountManagementService>();
            var controller = new HomeController(mockedAccountManagementService.Object);

            var loggedUserId = "userId";
            var loggedUserAvatarUrl = "avatar";
            controller.GetLoggedUserId = () => loggedUserId;

            mockedAccountManagementService.Setup(x => x.GetUserAvatarUrl(loggedUserId))
                .Returns(loggedUserAvatarUrl);

            // Act and Assert
            controller.WithCallTo(c => c.LoginPartial())
              .ShouldRenderPartialView("_LoginPartial")
              .WithModel(loggedUserAvatarUrl);
        }
    }
}
