using AutoMapper;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Web.Models.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Providers.Mapping.Profiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CarBasicInfo, CarViewModel>().ReverseMap();
            CreateMap<TagInfo, TagViewModel>().ReverseMap();

            CreateMap<TripCreationInfo, CreateTripViewModel>().ReverseMap();
        }
    }
}
