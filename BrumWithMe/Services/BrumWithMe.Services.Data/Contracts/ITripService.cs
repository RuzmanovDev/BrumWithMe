using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using System.Collections.Generic;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ITripService
    {
        void CreateTrip(TripCreationInfo tripInfo);

        TripDetails GetTripDetails(int tripId);

        IEnumerable<TripBasicInfo> GetLatestTripsBasicInfo(int v);

        TripSearchResult GetTripsFor(string origin, string destination, int page = 0);

        IEnumerable<TripBasicInfo> GetTripsCreatedByUser(string userId);
        bool RequestToJoinTrip(int tripId, string userId);

        bool isUserInTrip(string userId, int tripId);

        bool SignOutOfTrip(int tripId, string userId);
    }
}
