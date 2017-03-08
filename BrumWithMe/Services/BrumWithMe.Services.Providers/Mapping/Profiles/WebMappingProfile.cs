﻿using AutoMapper;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Web.Models.Trip;

namespace BrumWithMe.Services.Providers.Mapping.Profiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CarBasicInfo, CarViewModel>().ReverseMap();
            CreateMap<TagInfo, TagViewModel>().ReverseMap();

            CreateMap<TripCreationInfo, CreateTripViewModel>().ReverseMap();
            CreateMap<TripDetails, TripDetailsViewModel>().ReverseMap();
            CreateMap<TripBasicInfo, TripBasicInfoViewModel>();
        }
    }
}
