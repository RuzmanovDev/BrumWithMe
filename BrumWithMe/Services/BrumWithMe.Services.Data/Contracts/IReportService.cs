using BrumWithMe.Data.Models.CompositeModels.Trip;
using System.Collections.Generic;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface IReportService
    {
        void ReportTrip(int tripId);

        IEnumerable<TripBasicInfo> GetReportedTrips();
        void UnReportTrip(int tripId);
    }
}
