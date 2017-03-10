using System.Web.Mvc;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.MVC.Areas.Api.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            Guard.WhenArgument(cityService, nameof(cityService)).IsNull().Throw();

            this.cityService = cityService;
        }

        public ActionResult All()
        {
            var cities = this.cityService.GetAllCityNames();

            var result = new { data = cities };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}