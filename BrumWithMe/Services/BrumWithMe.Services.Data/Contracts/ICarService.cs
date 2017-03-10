using BrumWithMe.Data.Models.CompositeModels;
using System.Collections.Generic;
using BrumWithMe.Data.Models.Entities;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ICarService
    {
        IEnumerable<CarBasicInfo> GetUserCars(string userId);

        void AddCarToUser(Car car, string userId);
    }
}
