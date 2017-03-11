using System.Collections.Generic;

namespace BrumWithMe.Data.Models.CompositeModels.Trip
{
    public class TripSearchResult
    {
        public IEnumerable<TripBasicInfo> FoundTrips { get; set; }

        public int TotalTrips { get; set; }
    }
}
