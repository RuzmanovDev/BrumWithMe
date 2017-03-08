using BrumWithMe.Data.Models.TransportEntities;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ITripService
    {
        void CreateTrip(TripCreationInfo tripInfo);
    }
}
