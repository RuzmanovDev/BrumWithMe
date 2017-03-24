using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Web.Models.Areas.Admin;
using BrumWithMe.Web.Models.Trip;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrumWithMe.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IReportService reportService;
        private readonly IMappingProvider mappingProvider;
        private readonly ITripService tripService;

        public AdminController(IReportService reportService, IMappingProvider mappingProvider, ITripService tripService)
        {
            Guard.WhenArgument(reportService, nameof(reportService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.reportService = reportService;
            this.mappingProvider = mappingProvider;
            this.tripService = tripService;
        }

        public ActionResult Dashboard()
        {
            return this.View();
        }

        public ActionResult ReportedTrips()
        {
            var model = new ReportedTripsViewModel();
            var trips = this.reportService.GetReportedTrips();
            var tripsvmodel = this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(trips);

            model.ReportedTrips = tripsvmodel;
            return this.PartialView("_ReportedTrips", model);
        }

        public ActionResult DeletedTrips()
        {
            var deletedTrips = this.tripService.GetDeletedTrips();
            var tripsvmodel = this.mappingProvider.Map<IEnumerable<TripBasicInfo>, IEnumerable<TripBasicInfoViewModel>>(deletedTrips);

            return this.PartialView("_DeletedTrips", tripsvmodel);

        }

        [HttpPost]
        public ActionResult DeleteTrip(int tripId)
        {
            this.tripService.DeleteTrip(tripId);

            return this.RedirectToAction(nameof(AdminController.ReportedTrips));
        }

        [HttpPost]
        public ActionResult RestoreTrip(int tripId)
        {
            this.tripService.RecoverTrip(tripId);

            return this.RedirectToAction(nameof(AdminController.DeletedTrips));
        }

        [HttpPost]
        public ActionResult UnReportTrip(int tripId)
        {
            this.reportService.UnReportTrip(tripId);

            return this.RedirectToAction(nameof(AdminController.ReportedTrips));
        }
    }
}