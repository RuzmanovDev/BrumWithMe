using System;
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
using BrumWithMe.Data.Models.Enums;

namespace BrumWithMe.Services.Data.Services
{
    public class TripService : BaseDataService, ITripService
    {
        private readonly IProjectableRepositoryEf<Trip> tripRepo;
        private readonly IProjectableRepositoryEf<UsersTrips> userTripsRepo;
        private readonly ICityService cityService;
        private readonly ITagService tagService;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IMappingProvider mappingProvider;

        public TripService(
            Func<IUnitOfWorkEF> unitOfWork,
            IProjectableRepositoryEf<UsersTrips> userTripsRepo,
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
            this.userTripsRepo = userTripsRepo;
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
                    IsOwner = true,
                    UserTripStatusId = (int)UserTripStatusType.Accepted
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
            // takes the first set of sorted items
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

        public IEnumerable<TripInfoWithUserRequests> GetTripsCreatedByUser(string userId)
        {
            var tripIdsUserOwns = this.userTripsRepo.GetAll(x => x.IsOwner && x.UserId == userId, x => x.TripId);
            var tripsInfo = this.tripRepo.GetAllMapped<TripInfoWithUserRequests>(x => tripIdsUserOwns.Contains(x.Id) && !x.IsDeleted);

            return tripsInfo;
        }

        public bool RequestToJoinTrip(int tripId, string userId)
        {
            using (var uow = this.UnitOfWork())
            {
                bool isUserOwner = this.IsUserOwnerOfTrip(userId, tripId);

                if (isUserOwner)
                {
                    throw new InvalidOperationException("Trip owner cannot join trip created by him!");
                }

                this.userTripsRepo.Add(new UsersTrips()
                {
                    TripId = tripId,
                    UserId = userId,
                    IsOwner = false,
                    UserTripStatusId = (int)UserTripStatusType.Pending,
                });

                return uow.Commit();
            }
        }

        public bool SignOutOfTrip(int tripId, string userId)
        {
            using (var uow = this.UnitOfWork())
            {
                var userTrip = this.userTripsRepo.GetFirst(x => x.TripId == tripId);

                if (userTrip.UserTripStatusId == (int)UserTripStatusType.Accepted)
                {
                    var trip = userTrip.Trip;
                    trip.TakenSeats--;

                    if (trip.TakenSeats < 0)
                    {
                        throw new InvalidOperationException("The number of taken seats cannot be negative!");
                    }
                }

                this.userTripsRepo.Delete(userTrip);

                return uow.Commit();
            }
        }

        public bool IsPassengerInTrip(string passangerId, int tripId)
        {
            var fountTrip = this.userTripsRepo
                .GetFirst(x => x.UserId == passangerId && x.TripId == tripId && !x.IsOwner);

            bool isPassangerInTrip = false;
            if (fountTrip != null)
            {
                isPassangerInTrip = true;
            }

            return isPassangerInTrip;
        }

        public TripInfoWithUserRequests JoinUserToTrip(string userId, int tripId)
        {
            using (var uow = base.UnitOfWork())
            {
                bool isUserOwner = this.IsUserOwnerOfTrip(userId, tripId);

                if (isUserOwner)
                {
                    throw new InvalidOperationException("Trip owner cannot join trip created by him!");
                }

                var trip = this.tripRepo
                    .GetFirst(x => !x.IsDeleted && x.Id == tripId);

                var incrementedSeats = ++trip.TakenSeats;

                if (incrementedSeats > trip.TotalSeats)
                {
                    throw new InvalidOperationException("There aren't any more free seats!");
                }

                this.userTripsRepo.Update(new UsersTrips()
                {
                    UserId = userId,
                    TripId = tripId,
                    IsOwner = false,
                    UserTripStatusId = (int)UserTripStatusType.Accepted
                });

                uow.Commit();

                var tripInfo = this.tripRepo.GetFirstMapped<TripInfoWithUserRequests>(x => x.Id == tripId && !x.IsDeleted);

                return tripInfo;
            }
        }

        public TripInfoWithUserRequests RejectUserToJoinTrip(string userId, int tripId)
        {
            using (var uow = base.UnitOfWork())
            {
                this.userTripsRepo.Update(new UsersTrips()
                {
                    UserId = userId,
                    TripId = tripId,
                    UserTripStatusId = (int)UserTripStatusType.Declined
                });

                uow.Commit();

                var trip = this.tripRepo.GetFirstMapped<TripInfoWithUserRequests>(x => x.Id == tripId);

                return trip;
            }
        }

        private bool IsUserOwnerOfTrip(string userId, int tripId)
        {
            var userTrip = this.userTripsRepo.GetFirst(x => x.UserId == userId && x.TripId == tripId && x.IsOwner);
            if (userTrip != null)
            {
                return true;
            }

            return false;
        }
    }
}
