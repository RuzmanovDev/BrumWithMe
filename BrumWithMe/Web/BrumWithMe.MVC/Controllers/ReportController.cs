using System.Web.Mvc;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            Guard.WhenArgument(reportService, nameof(reportService)).IsNull().Throw();

            this.reportService = reportService;
        }

        [HttpPost]
        public ActionResult ReportTrip(int tripId)
        {
            this.reportService.ReportTrip(tripId);

            return View();
        }
        
    }
}