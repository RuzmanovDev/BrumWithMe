using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Services.Data.Services;

namespace BrumWithMe.Services.Data.Tests.BaseDataServiceTests.Mocks
{
    public class DerivedDataService : BaseDataService
    {
        public DerivedDataService(Func<IUnitOfWorkEF> unitOfWork)
            : base(unitOfWork)
        {
        }

        public Func<IUnitOfWorkEF> GetUnitOfWork => this.UnitOfWork;
    }
}
