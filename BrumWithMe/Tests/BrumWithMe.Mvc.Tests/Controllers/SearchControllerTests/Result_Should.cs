using System.Collections.Generic;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Search;
using BrumWithMe.Web.Models.Trip;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Result_Should
    {
        [Test]
        public void ReturnDefaulView_WithCorrectViewModel()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new SearchController(mockedTripService.Object, mockedMappingProvider.Object);

            var searchViewModel = new SearchTripViewModel();
            searchViewModel.Origin = "Pazardzhik";
            searchViewModel.Destination = "Sofia";

            var tripsBasinInfo = new TripSearchResult();
            tripsBasinInfo.FoundTrips = new List<TripBasicInfo>();

            mockedTripService.Setup(x => x.GetTripsFor(searchViewModel.Origin, searchViewModel.Destination, 0))
                .Returns(tripsBasinInfo);


            var tripsViewModel = new List<TripBasicInfoViewModel>();
            mockedMappingProvider.Setup(x => x.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(tripsBasinInfo.FoundTrips))
                .Returns(tripsViewModel);

            // Act and Assert
            controller.WithCallTo(x => x.Result(searchViewModel))
                .ShouldRenderDefaultView()
                .WithModel(searchViewModel);
        }
    }
}
