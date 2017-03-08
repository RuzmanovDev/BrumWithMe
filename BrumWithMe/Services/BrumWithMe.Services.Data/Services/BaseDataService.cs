using System;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Services
{
    public abstract class BaseDataService
    {
        private readonly Func<IUnitOfWork> unitOfWork;
        private readonly IMappingProvider mappingProvider;

        protected BaseDataService(Func<IUnitOfWork> unitOfWork, IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(mappingProvider)).IsNull().Throw();

            this.unitOfWork = unitOfWork;
            this.mappingProvider = mappingProvider;
        }

        protected Func<IUnitOfWork> UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        protected IMappingProvider MappingProvider
        {
            get { return this.mappingProvider; }
        }
    }
}
