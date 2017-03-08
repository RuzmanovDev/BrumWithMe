using System;
using System.Collections.Generic;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;
using System.Linq;
using BrumWithMe.Services.Providers.TimeProviders;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Data.Models.TransportEntities.Trip;

namespace BrumWithMe.Services.Data.Services
{
    public class TripService : BaseDataService, ITripService
    {
        private readonly IRepository<Trip> tripRepo;
        private readonly ICityService cityService;
        private readonly ITagService tagService;
        private readonly IDateTimeProvider dateTimeProvider;

        public TripService(
            Func<IUnitOfWork> unitOfWork,
            ICityService cityService,
            IMappingProvider mappingProvider,
            ITagService tagService,
            IRepository<Trip> tripRepo,
            IDateTimeProvider dateTimeProvider)
            : base(unitOfWork,mappingProvider)
        {
            Guard.WhenArgument(tripRepo, nameof(tripRepo)).IsNull().Throw();
            Guard.WhenArgument(cityService, nameof(cityService)).IsNull().Throw();
            Guard.WhenArgument(tagService, nameof(tagService)).IsNull().Throw();

            this.tripRepo = tripRepo;
            this.cityService = cityService;
            this.tagService = tagService;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void CreateTrip(TripCreationInfo tripInfo)
        {
            var origin = this.cityService.GetCityByName(tripInfo.OriginName);

            if (origin == null)
            {
                origin = this.cityService.CreateCity(tripInfo.OriginName);
            }

            var destination = this.cityService.GetCityByName(tripInfo.DestinationName);

            if (destination == null)
            {
                destination = this.cityService.CreateCity(tripInfo.DestinationName);
            }

            var tags = this.tagService.GetTagsByIds(tripInfo.TagIds) as ICollection<Tag>;

            using (var uow = base.UnitOfWork())
            {
                var trip = base.MappingProvider.Map<TripCreationInfo, Trip>(tripInfo);

                trip.Tags = tags;
                trip.Origin = origin;
                trip.Destination = destination;
                trip.DateCreated = this.dateTimeProvider.Now;

                var userTrips = new UsersTrips()
                {
                    Trip = trip,
                    UserId = tripInfo.DriverId,
                    IsDriver = true
                };

                trip.TripsUsers = new List<UsersTrips>() { userTrips };

                this.tripRepo.Add(trip);

                uow.Commit();
            }
        }

        public TripDetails GetTripDetails(int tripId)
        {
            var trip = this.tripRepo.GetFirst(x => x.Id == tripId,
                x => x.TripsUsers,
                x => x.Car,
                x => x.Destination,
                x => x.Origin,
                x => x.Tags);

            var resut = base.MappingProvider.Map<Trip, TripDetails>(trip);
            return resut;
        }

        public IEnumerable<TripBasicInfo> GetLatestTripsBasicInfo(int countToTake)
        {
            var trips = this.tripRepo
                .GetAll(x => true, x => x, i => i.Car.Owner, i => i.Destination, i => i.Origin)
                .OrderByDescending(x => x.DateCreated)
                .Take(countToTake);

            var result = base.MappingProvider.Map<IEnumerable<Trip>, IEnumerable<TripBasicInfo>>(trips);

            return result;
        }
    }
}
