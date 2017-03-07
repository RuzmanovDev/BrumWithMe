using BrumWithMe.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface ICityService
    {
        City GetCityByName(string cityName);

        City CreateCity(string cityName);
    }
}
