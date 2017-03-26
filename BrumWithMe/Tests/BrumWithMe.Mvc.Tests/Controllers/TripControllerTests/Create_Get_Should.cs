using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.TripControllerTests
{
    [TestFixture]
    public class Create_Get_Should
    {
        [Test]
        public void Have_AuthorizeAttribute()
        {
            // Arrange
            var method = typeof(TripController).GetMethod(nameof(TripController.Create), new Type[0]);

            // Act
            var hasAttr = method.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any();

            // Assert
            Assert.IsTrue(hasAttr);
        }

        [Test]
        public void RedirectTo_CreateCarPage_WhenuserDoesNotHaveAnyRegisteredCars()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new TripController(
                     mockedTripService.Object,
                     mockedTagService.Object,
                     mockedCarService.Object,
                     mockedMappingProvider.Object);

            mockedTagService.Setup(x => x.GetAllTags())
                .Returns(new List<TagInfo>());

            var userId = "userId";
            controller.GetLoggedUserId = () => userId;

            var cars = new List<CarBasicInfo>();
            mockedCarService.Setup(x => x.GetUserCars(userId))
                .Returns(cars);

            // Act and Assert
            controller.WithCallTo(x => x.Create())
                 .ShouldRedirectTo((ManageController c) => c.RegisterCar());
        }

        [Test]
        public void RedirectTo_CreateCarPage_WhenuserCarsAreNull()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new TripController(
                     mockedTripService.Object,
                     mockedTagService.Object,
                     mockedCarService.Object,
                     mockedMappingProvider.Object);

            mockedTagService.Setup(x => x.GetAllTags())
                .Returns(new List<TagInfo>());

            var userId = "userId";
            controller.GetLoggedUserId = () => userId;

            List<CarBasicInfo> cars = null;
            mockedCarService.Setup(x => x.GetUserCars(userId))
                .Returns(cars);

            // Act and Assert
            controller.WithCallTo(x => x.Create())
                 .ShouldRedirectTo((ManageController c) => c.RegisterCar());
        }

        [Test]
        public void ReturnDefaultView_WithCorrectViewModel()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedTagService = new Mock<ITagService>();
            var mockedCarService = new Mock<ICarService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new TripController(
                     mockedTripService.Object,
                     mockedTagService.Object,
                     mockedCarService.Object,
                     mockedMappingProvider.Object);

            mockedTagService.Setup(x => x.GetAllTags())
            .Returns(new List<TagInfo>());

            var userId = "userId";
            controller.GetLoggedUserId = () => userId;

            List<CarBasicInfo> cars = new List<CarBasicInfo>() { new CarBasicInfo() };
            mockedCarService.Setup(x => x.GetUserCars(userId))
                .Returns(cars);


            // Act and Assert
            controller.WithCallTo(x => x.Create())
                .ShouldRenderDefaultView()
                .WithModel<CreateTripViewModel>();

        }
    }
}
