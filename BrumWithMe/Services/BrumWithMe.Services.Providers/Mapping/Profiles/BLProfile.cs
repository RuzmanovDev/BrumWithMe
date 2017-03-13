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

            CreateMap<UsersTrips, TripBasicInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Trip.Id))
                .ForMember(dest => dest.OriginName, opt => opt.MapFrom(src => src.Trip.Origin.Name))
                .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.Trip.Destination.Name))
                .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.Trip.TotalSeats))
                .ForMember(dest => dest.TakenSeats, opt => opt.MapFrom(src => src.Trip.TakenSeats))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Trip.Price))
                .ForMember(dest => dest.TimeOfDeparture, opt => opt.MapFrom(src => src.Trip.TimeOfDeparture))
                .ForMember(dest => dest.UserAvatarImageUrl, opt => opt.MapFrom(src => src.User.AvataImageurl));
        }
    }
}
