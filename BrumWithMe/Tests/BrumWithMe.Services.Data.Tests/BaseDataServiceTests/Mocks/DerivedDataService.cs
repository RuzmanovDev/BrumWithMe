using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Tests.BaseDataServiceTests.Mocks
{
    public class DerivedDataService : BaseDataService
    {
        public DerivedDataService(Func<IUnitOfWork> unitOfWork, IMappingProvider mappingProvider)
            : base(unitOfWork, mappingProvider)
        {
        }

        public Func<IUnitOfWork> GetUnitOfWork => this.UnitOfWork;

        public IMappingProvider GetMappingProvider => this.MappingProvider;
    }
}
