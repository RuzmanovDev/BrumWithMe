using System;
using System.Collections.Generic;
using System.Linq;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Data.Models.Enums;

namespace BrumWithMe.Services.Data.Services
{
    public class UserDashboardService : BaseDataService, IUserDashboardService
    {
        private readonly IProjectableRepositoryEf<Trip> tripRepo;
        private readonly IProjectableRepositoryEf<UsersTrips> userTripsRepo;
        private readonly IMappingProvider mappingProvider;
        private readonly ITripService tripService;

        public UserDashboardService(
            Func<IUnitOfWorkEF> unitOfWork,
            IProjectableRepositoryEf<UsersTrips> userTripsRepo,
            IMappingProvider mappingProvider,
            ITripService tripService,
            IProjectableRepositoryEf<Trip> tripRepo)
            : base(unitOfWork)
        {
            Guard.WhenArgument(tripRepo, nameof(tripRepo)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.tripRepo = tripRepo;
            this.mappingProvider = mappingProvider;
            this.userTripsRepo = userTripsRepo;
            this.tripService = tripService;
        }

        public IEnumerable<TripInfoWithUserRequests> GetTripsCreatedByUser(string userId)
        {
            var tripIdsUserOwns =
                this.userTripsRepo.GetAll(
                    x => x.UserTripStatusId == (int)UserTripStatusType.Owner
                    && x.UserId == userId,
                    x => x.TripId);

            var tripsInfo = this.tripRepo.GetAllMapped<TripInfoWithUserRequests>(x => tripIdsUserOwns.Contains(x.Id) && !x.IsDeleted && !x.IsFinished);

            return tripsInfo;
        }

        public IEnumerable<TripBasicInfoWithStatus> GetTripsJoinedByUser(string userId)
        {
            var result = this.userTripsRepo.GetAllMapped<TripBasicInfoWithStatus>(x => x.UserId == userId && x.UserTripStatusId != (int)UserTripStatusType.Owner);

            return result;
        }

        public TripInfoWithUserRequests JoinUserToTrip(string userId, int tripId)
        {
            using (var uow = base.UnitOfWork())
            {
                bool isUserOwner = this.tripService.IsUserOwnerOfTrip(userId, tripId);

                if (isUserOwner)
                {
                    throw new InvalidOperationException("Trip owner cannot join trip created by him!");
                }

                var trip = this.tripRepo
                    .GetFirst(x => !x.IsDeleted && !x.IsFinished && x.Id == tripId);

                var incrementedSeats = ++trip.TakenSeats;

                if (incrementedSeats > trip.TotalSeats)
                {
                    throw new InvalidOperationException("There aren't any more free seats!");
                }

                this.userTripsRepo.Update(new UsersTrips()
                {
                    UserId = userId,
                    TripId = tripId,
                    UserTripStatusId = (int)UserTripStatusType.Accepted
                });

                uow.Commit();

                var tripInfo = this.tripRepo.GetFirstMapped<TripInfoWithUserRequests>(x => x.Id == tripId && !x.IsDeleted && !x.IsFinished);

                return tripInfo;
            }
        }

        public TripInfoWithUserRequests RejectUserToJoinTrip(string userId, int tripId)
        {
            this.tripService.SignOutOfTrip(tripId, userId);

            var updatedTripInfo = this.tripRepo.GetFirstMapped<TripInfoWithUserRequests>(x => x.Id == tripId);

            return updatedTripInfo;

        }
    }
}
