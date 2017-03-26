using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Areas.Admin.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Areas.Admin.Controllers.AdminControllerTests
{
    [TestFixture]
    public class DeletedTrips_Should
    {
        [Test]
        public void Call_DeletedTrips_Partial_WithDataFromTheService()
        {
            // Arrange
            var mockedReportService = new Mock<IReportService>();
            var mockedMapingProvider = new Mock<IMappingProvider>();
            var mockedTripService = new Mock<ITripService>();

            var controller = new AdminController(mockedReportService.Object, mockedMapingProvider.Object, mockedTripService.Object);

            var tripsFromService = new List<TripBasicInfo>();
            mockedTripService.Setup(x => x.GetDeletedTrips())
                .Returns(tripsFromService);

            var tripsViewModel = new List<TripBasicInfoViewModel>();
            mockedMapingProvider.Setup(x => x.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(tripsFromService))
                .Returns(tripsViewModel);

            // Act and Assert
            controller.WithCallTo(x => x.DeletedTrips())
                .ShouldRenderPartialView("_DeletedTrips")
                .WithModel(tripsViewModel);
        }
    }
}
