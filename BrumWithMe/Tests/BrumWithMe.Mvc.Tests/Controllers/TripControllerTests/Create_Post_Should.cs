using System.Collections.Generic;
using System.Linq;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.TripControllerTests
{
    [TestFixture]
    public class Create_Post_Should
    {
        [Test]
        public void ReturnDefaulView_WhenModelStateIsNotValid()
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

            controller.ModelState.AddModelError("", "");
            var viewModel = new CreateTripViewModel();

            // Act and Assert
            controller.WithCallTo(x => x.Create(viewModel))
                .ShouldRenderDefaultView()
                .WithModel(viewModel);
        }

        [Test]
        public void PassCorrectTripInfo_ToTripService()
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

            string loggedUserId = "LoggedusrId";
            controller.GetLoggedUserId = () => loggedUserId;

            var viewModel = new CreateTripViewModel()
            {
                HourOfDeparture = "12:14"
            };

            var tripCreationInfo = new TripCreationInfo();
            mockedMappingProvider.Setup(x => x.Map<CreateTripViewModel, TripCreationInfo>(viewModel))
                .Returns(tripCreationInfo);

            // Act and Assert
            controller.WithCallTo(x => x.Create(viewModel))
                .ShouldRedirectTo(x => x.Create());

            mockedTripService.Verify(x => x.CreateTrip(tripCreationInfo), Times.Once);
        }

        [Test]
        public void AtachOnlySelectedTagsToTheModel()
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

            string loggedUserId = "LoggedusrId";
            controller.GetLoggedUserId = () => loggedUserId;

            var viewModel = new CreateTripViewModel()
            {
                HourOfDeparture = "12:14",
                Tags = new List<TagViewModel>() { new TagViewModel() { Id = 1, IsSelected = true }, new TagViewModel() { Id = 2, IsSelected = false }, }
            };

            var tripCreationInfo = new TripCreationInfo();
            mockedMappingProvider.Setup(x => x.Map<CreateTripViewModel, TripCreationInfo>(viewModel))
                .Returns(tripCreationInfo);

            // Act and Assert
            controller.WithCallTo(x => x.Create(viewModel))
                .ShouldRedirectTo(x => x.Create());

            mockedTripService.Verify(x => x.CreateTrip(tripCreationInfo), Times.Once);
            Assert.AreEqual(1, tripCreationInfo.TagIds.Count());
        }

        [Test]
        public void AttachEmptyCollection_IfSelectedTagsAreNull()
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

            string loggedUserId = "LoggedusrId";
            controller.GetLoggedUserId = () => loggedUserId;

            var viewModel = new CreateTripViewModel()
            {
                HourOfDeparture = "12:14",
                Tags = null
            };

            var tripCreationInfo = new TripCreationInfo();
            mockedMappingProvider.Setup(x => x.Map<CreateTripViewModel, TripCreationInfo>(viewModel))
                .Returns(tripCreationInfo);

            // Act and Assert
            controller.WithCallTo(x => x.Create(viewModel))
                .ShouldRedirectTo(x => x.Create());

            mockedTripService.Verify(x => x.CreateTrip(tripCreationInfo), Times.Once);
            Assert.AreEqual(0, tripCreationInfo.TagIds.Count());
        }
    }
}
