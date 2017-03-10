using BrumWithMe.Data.Models.Entities;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ICityService
    {
        City GetCityByName(string cityName);

        City CreateCity(string cityName);
    }
}
