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
    }
}
