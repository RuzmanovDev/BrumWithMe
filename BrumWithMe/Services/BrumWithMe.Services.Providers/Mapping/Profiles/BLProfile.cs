using AutoMapper;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Web.Models.Shared;

namespace BrumWithMe.Services.Providers.Mapping.Profiles
{
    public class BLProfile : Profile
    {
        public BLProfile()
        {
            CreateMap<TripCreationInfo, Trip>();
            CreateMap<Trip, TripDetails>()
                 .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => src.Car.Owner));

            CreateMap<Trip, TripBasicInfo>()
                .ForMember(dest => dest.UserAvatarImageUrl, opt => opt.MapFrom(src => src.Car.Owner.AvataImageurl))
                .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.Destination.Name))
                .ForMember(dest => dest.OriginName, opt => opt.MapFrom(src => src.Origin.Name));

            CreateMap<Tag, TagInfo>().ReverseMap();

            CreateMap<Car, CarBasicInfo>();

            CreateMap<User, UserBasicInfo>();
        }
    }
}
