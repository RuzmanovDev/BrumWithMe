using BrumWithMe.MVC.Controllers;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Search;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace BrumWithMe.Mvc.Tests.Controllers.SearchControllerTests
{
    [TestFixture]
    public class Search_Should
    {
        [Test]
        public void Return_TripQuickSerachPartial()
        {
            // Arrange
            var mockedTripService = new Mock<ITripService>();
            var mockedMappingProvider = new Mock<IMappingProvider>();

            var controller = new SearchController(mockedTripService.Object, mockedMappingProvider.Object);

            // Act and Assert
            controller.WithCallToChild(x => x.Search())
                .ShouldRenderPartialView("_TripQuickSearch")
                .WithModel<SearchTripViewModel>();
        }
    }
}
