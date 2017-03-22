using BrumWithMe.Web.Models.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Web.Models.Areas.Admin
{
    public class ReportedTripsViewModel
    {
        public IEnumerable<TripBasicInfoViewModel> ReportedTrips { get; set; }
    }
}
