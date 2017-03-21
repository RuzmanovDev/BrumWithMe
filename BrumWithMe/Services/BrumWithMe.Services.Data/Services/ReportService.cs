using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using Bytes2you.Validation;
using BrumWithMe.Services.Data.Contracts;

namespace BrumWithMe.Services.Data.Services
{
    public class ReportService : BaseDataService, IReportService
    {
        private readonly IRepositoryEf<Trip> tripRepo;

        public ReportService(IRepositoryEf<Trip> tripRepo, Func<IUnitOfWorkEF> unitOfWork)
            : base(unitOfWork)
        {
            Guard.WhenArgument(tripRepo, nameof(tripRepo)).IsNull().Throw();

            this.tripRepo = tripRepo;
        }

        public void ReportTrip(int tripId)
        {
            var trip = this.tripRepo.GetFirst(x => x.Id == tripId);

            using (var uow = base.UnitOfWork())
            {
                trip.IsReported = true;

                uow.Commit();
            }
        }
    }
}
