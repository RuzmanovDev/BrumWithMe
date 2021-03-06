﻿using AutoMapper;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Web.Models.Shared;
using System.Linq;
using BrumWithMe.Data.Models.Enums;
using BrumWithMe.Data.Models.CompositeModels.Review;
using System;

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
                .ForMember(dest => dest.UserAvatarImageUrl, opt => opt.MapFrom(src => src.Car.Owner.AvatarImageurl))
                .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.Destination.Name))
                .ForMember(dest => dest.OriginName, opt => opt.MapFrom(src => src.Origin.Name));

            CreateMap<Tag, TagInfo>().ReverseMap();

            CreateMap<Car, CarBasicInfo>();

            CreateMap<User, UserBasicInfo>()
                     .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.LockoutEndDateUtc != null ? (DateTime)src.LockoutEndDateUtc > DateTime.Now : false))
                     .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.ReviewsByHim
                        .Where(x => x.ReviewedUserId == src.Id).Select(x => x.Rating).Average()));


            CreateMap<UsersTrips, TripBasicInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Trip.Id))
                .ForMember(dest => dest.OriginName, opt => opt.MapFrom(src => src.Trip.Origin.Name))
                .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.Trip.Destination.Name))
                .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.Trip.TotalSeats))
                .ForMember(dest => dest.TakenSeats, opt => opt.MapFrom(src => src.Trip.TakenSeats))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Trip.Price))
                .ForMember(dest => dest.TimeOfDeparture, opt => opt.MapFrom(src => src.Trip.TimeOfDeparture))
                .ForMember(dest => dest.UserAvatarImageUrl, opt => opt.MapFrom(src => src.User.AvatarImageurl));


            CreateMap<UsersTrips, PassangerInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.UserTripStatus.Name));


            CreateMap<Trip, TripInfoWithUserRequests>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OriginName, opt => opt.MapFrom(src => src.Origin.Name))
                .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.Destination.Name))
                .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.TotalSeats))
                .ForMember(dest => dest.TakenSeats, opt => opt.MapFrom(src => src.TakenSeats))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.TimeOfDeparture, opt => opt.MapFrom(src => src.TimeOfDeparture))
                .ForMember(dest => dest.CarAvatarImage, opt => opt.MapFrom(src => src.Car.ImageUrl));
            //.ForMember(dest => dest.Passangers, opt => opt.MapFrom(src => src.TripsUsers.Where(x => x.UserTripStatusId != (int)UserTripStatusType.Owner)));

            CreateMap<UsersTrips, TripBasicInfoWithStatus>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TripId))
               .ForMember(dest => dest.OriginName, opt => opt.MapFrom(src => src.Trip.Origin.Name))
               .ForMember(dest => dest.DestinationName, opt => opt.MapFrom(src => src.Trip.Destination.Name))
               .ForMember(dest => dest.TotalSeats, opt => opt.MapFrom(src => src.Trip.TotalSeats))
               .ForMember(dest => dest.TakenSeats, opt => opt.MapFrom(src => src.Trip.TakenSeats))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Trip.Price))
               .ForMember(dest => dest.TimeOfDeparture, opt => opt.MapFrom(src => src.Trip.TimeOfDeparture))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.UserTripStatus.Name));

            CreateMap<Review, CommentInfo>()
               .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Creator));
        }
    }
}
