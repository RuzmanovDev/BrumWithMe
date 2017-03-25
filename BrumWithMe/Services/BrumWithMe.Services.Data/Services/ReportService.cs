using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using Bytes2you.Validation;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels.Trip;
using System.Collections.Generic;

namespace BrumWithMe.Services.Data.Services
{
    public class ReportService : BaseDataService, IReportService
    {
        private readonly IProjectableRepositoryEf<Trip> tripRepo;

        public ReportService(IProjectableRepositoryEf<Trip> tripRepo, Func<IUnitOfWorkEF> unitOfWork)
            : base(unitOfWork)
        {
            Guard.WhenArgument(tripRepo, nameof(tripRepo)).IsNull().Throw();

            this.tripRepo = tripRepo;
        }

        public void ReportTrip(int tripId)
        {
            var trip = this.tripRepo.GetFirst(x => x.Id == tripId);

            if (trip == null)
            {
                return;
            }

            using (var uow = base.UnitOfWork())
            {
                trip.IsReported = true;

                uow.Commit();
            }
        }

        public void UnReportTrip(int tripId)
        {
            var trip = this.tripRepo.GetFirst(x => x.Id == tripId);

            if (trip == null)
            {
                return;
            }

            using (var uow = base.UnitOfWork())
            {
                trip.IsReported = false;

                uow.Commit();
            }
        }

        public IEnumerable<TripBasicInfo> GetReportedTrips()
        {
            return this.tripRepo.GetAllMapped<TripBasicInfo>(x => x.IsReported && !x.IsDeleted);
        }
    }
}
