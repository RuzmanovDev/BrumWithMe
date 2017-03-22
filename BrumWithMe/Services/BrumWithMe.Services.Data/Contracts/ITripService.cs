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

        IEnumerable<PassangerInfo> GetPassengersForTheTrip(int tripId);

        IEnumerable<TripBasicInfo> GetDeletedTrips();

        bool RequestToJoinTrip(int tripId, string userId);

        bool IsPassengerInTrip(string userId, int tripId);

        bool SignOutOfTrip(int tripId, string userId);

        bool IsUserOwnerOfTrip(string userId, int tripId);

        bool MarkTripAsFinished(int tripId, string userId);

        bool DeleteTrip(int tripId);

        bool RecoverTrip(int tripId);
    }
}
