using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Web.Models.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ITripService
    {
        void AddTrip(TripCreationInfo tripInfo);

        void CreateTrip(TripCreationInfo tripInfo);
    }
}
