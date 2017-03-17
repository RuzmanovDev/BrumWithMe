using BrumWithMe.Data.Models.CompositeModels.Trip;
using System.Collections.Generic;

namespace BrumWithMe.Web.Models.Dashboard
{
    public class DashboardViewModel
    {
        public IEnumerable<TripInfoWithUserRequests> TripsCreatedByUser { get; set; }
    }
}
