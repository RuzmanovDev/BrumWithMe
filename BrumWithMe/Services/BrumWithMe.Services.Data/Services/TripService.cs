using System;
using System.Collections.Generic;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Web.Models.Trip;
using System.Linq;

namespace BrumWithMe.Services.Data.Services
{
    public class TripService : BaseDataService, ITripService
    {
        private readonly IRepository<Trip> tripRepo;
        private readonly ICityService cityService;
        private readonly ITagService tagService;

        public TripService(Func<IUnitOfWork> unitOfWork,
            IRepository<Trip> tripRepo)
            : base(unitOfWork)
        {
            Guard.WhenArgument(tripRepo, nameof(tripRepo)).IsNull().Throw();

            this.tripRepo = tripRepo;
        }

        public void AddTrip(TripCreationInfo tripInfo)
        {
            Guard.WhenArgument(tripInfo, nameof(tripInfo)).IsNull().Throw();

            using (var uow = base.UnitOfWork())
            {
                var trip = new Trip()
                {
                    CarId = tripInfo.CarId,
                    Date = tripInfo.TimeOfDeparture,
                    Description = tripInfo.Description,
                    Destination = tripInfo.Destination,
                    Origin = tripInfo.Origin,
                    Price = tripInfo.Price,
                    TotalSeats = tripInfo.FreeSeats,
                    Tags = tripInfo.TagIds as ICollection<Tag>,
                };

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
                var trip = new Trip()
                {
                    CarId = tripInfo.CarId,
                    Date = tripInfo.TimeOfDeparture,
                    Description = tripInfo.Description,
                    Destination = destination,
                    Origin = origin,
                    Price = tripInfo.Price,
                    TotalSeats = tripInfo.FreeSeats,
                    Tags = tags
                };

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
    }
}
