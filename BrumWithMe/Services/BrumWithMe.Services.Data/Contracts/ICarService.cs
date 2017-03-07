using BrumWithMe.Data.Models.TransportEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ICarService
    {
        IEnumerable<CarBasicInfo> GetUserCars(string userId);
    }
}
