using BrumWithMe.Data.Models.CompositeModels.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface IUserDashboardService
    {
        IEnumerable<TripInfoWithUserRequests> GetTripsCreatedByUser(string userId);

        IEnumerable<TripBasicInfoWithStatus> GetTripsJoinedByUser(string userId);

        TripInfoWithUserRequests JoinUserToTrip(string userId, int tripId);

        TripInfoWithUserRequests RejectUserToJoinTrip(string userId, int tripId);

    }
}
