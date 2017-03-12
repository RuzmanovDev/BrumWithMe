﻿using System;
using System.Collections.Generic;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;
using System.Linq;
using BrumWithMe.Services.Providers.TimeProviders;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;

namespace BrumWithMe.Services.Data.Services
{
    public class TripService : BaseDataService, ITripService
    {
        private readonly IProjectableRepositoryEf<Trip> tripRepo;
        private readonly ICityService cityService;
        private readonly ITagService tagService;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMappingProvider mappingProvider;

        public TripService(
            Func<IUnitOfWorkEF> unitOfWork,
            ICityService cityService,
            IMappingProvider mappingProvider,
            ITagService tagService,
            IProjectableRepositoryEf<Trip> tripRepo,
            IDateTimeProvider dateTimeProvider)
            : base(unitOfWork)
        {
            Guard.WhenArgument(tripRepo, nameof(tripRepo)).IsNull().Throw();
            Guard.WhenArgument(cityService, nameof(cityService)).IsNull().Throw();
            Guard.WhenArgument(tagService, nameof(tagService)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.tripRepo = tripRepo;
            this.cityService = cityService;
            this.tagService = tagService;
            this.dateTimeProvider = dateTimeProvider;
            this.mappingProvider = mappingProvider;
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
                var trip = this.mappingProvider.Map<TripCreationInfo, Trip>(tripInfo);

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
            var tripDetails = this.tripRepo.GetFirstMapped<TripDetails>(x => x.Id == tripId);

            return tripDetails;
        }

        public IEnumerable<TripBasicInfo> GetLatestTripsBasicInfo(int countToTake)
        {
            // take the first set of sorted items
            int page = 0;

            var trips = this.tripRepo.GetAllMapped<DateTime, TripBasicInfo>(
                x => !x.IsDeleted, x => x.DateCreated, page, countToTake);

            return trips;
        }

        public TripSearchResult GetTripsFor(string origin, string destination, int page = 0)
        {
            origin = origin?.ToLower();
            destination = destination?.ToLower();

            int size = 2;

            // handle zero based paging
            page =
                page - 1 >= 0
                ?
                page - 1 : 0;

            int totalCount = this.tripRepo
                .GetAll(x => x.Origin.Name.ToLower().Contains(origin)
                && x.Destination.Name.ToLower().Contains(destination),
                x => x.Id, i => i.Destination, i => i.Origin)
                .Count();

            var trips = this.tripRepo
                .GetAllMapped<DateTime, TripBasicInfo>(
                where => where.Origin.Name.ToLower().Contains(origin)
                && where.Destination.Name.ToLower().Contains(destination),
                x => x.DateCreated, page, size);

            var result = new TripSearchResult();

            result.FoundTrips = trips;
            result.TotalTrips = totalCount;

            return result;
        }
    }
}
