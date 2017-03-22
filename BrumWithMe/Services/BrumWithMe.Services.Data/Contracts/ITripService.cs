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

        IEnumerable<TripInfoWithUserRequests> GetTripsCreatedByUser(string userId);

        IEnumerable<PassangerInfo> GetPassengersForTheTrip(int tripId);

        IEnumerable<TripBasicInfoWithStatus> GetTripsJoinedByUser(string userId);

        bool RequestToJoinTrip(int tripId, string userId);

        bool IsPassengerInTrip(string userId, int tripId);

        bool SignOutOfTrip(int tripId, string userId);

        TripInfoWithUserRequests JoinUserToTrip(string userId, int tripId);

        TripInfoWithUserRequests RejectUserToJoinTrip(string userId, int tripId);

        bool MarkTripAsFinished(int tripId, string userId);

        bool DeleteTrip(int tripId);
    }
}
