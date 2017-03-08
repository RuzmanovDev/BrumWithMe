using AutoMapper;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.TransportEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Providers.Mapping.Profiles
{
    public class BLProfile : Profile
    {
        public BLProfile()
        {
            CreateMap<TripCreationInfo, Trip>();
        }
    }
}
