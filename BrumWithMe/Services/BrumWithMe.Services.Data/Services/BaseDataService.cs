using System;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Services
{
    public abstract class BaseDataService
    {
        private readonly Func<IUnitOfWorkEF> unitOfWork;
        private readonly IMappingProvider mappingProvider;

        protected BaseDataService(Func<IUnitOfWorkEF> unitOfWork, IMappingProvider mappingProvider)
        {
            Guard.WhenArgument(unitOfWork, nameof(Func<IUnitOfWorkEF>)).IsNull().Throw();
            Guard.WhenArgument(mappingProvider, nameof(IMappingProvider)).IsNull().Throw();

            this.unitOfWork = unitOfWork;
            this.mappingProvider = mappingProvider;
        }

        protected Func<IUnitOfWorkEF> UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        protected IMappingProvider MappingProvider
        {
            get { return this.mappingProvider; }
        }
    }
}
