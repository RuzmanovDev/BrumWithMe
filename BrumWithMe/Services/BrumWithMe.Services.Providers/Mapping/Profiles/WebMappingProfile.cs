using AutoMapper;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Models.CompositeModels.Review;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Web.Models.Manage;
using BrumWithMe.Web.Models.Review;
using BrumWithMe.Web.Models.Shared;
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
            CreateMap<UserBasicInfo, UserBannerViewModel>();
            CreateMap<RegisterCarViewModel, Car>();

            CreateMap<PostCommentViewModel, Review>();

            CreateMap<CommentInfo, CommentViewModel>();
        }
    }
}
