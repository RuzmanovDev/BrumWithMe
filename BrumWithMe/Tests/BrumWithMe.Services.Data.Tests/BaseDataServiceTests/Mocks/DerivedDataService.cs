using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Tests.BaseDataServiceTests.Mocks
{
    public class DerivedDataService : BaseDataService
    {
        public DerivedDataService(Func<IUnitOfWorkEF> unitOfWork, IMappingProvider mappingProvider)
            : base(unitOfWork, mappingProvider)
        {
        }

        public Func<IUnitOfWorkEF> GetUnitOfWork => this.UnitOfWork;

        public IMappingProvider GetMappingProvider => this.MappingProvider;
    }
}
