using BrumWithMe.Data.Models.Entities;
using System.Collections.Generic;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ICityService
    {
        City GetCityByName(string cityName);

        City CreateCity(string cityName);

        IEnumerable<string> GetAllCityNames();
    }
}
