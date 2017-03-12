using System;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public abstract class BaseDataService
    {
        private readonly Func<IUnitOfWorkEF> unitOfWork;

        protected BaseDataService(Func<IUnitOfWorkEF> unitOfWork)
        {
            Guard.WhenArgument(unitOfWork, nameof(Func<IUnitOfWorkEF>)).IsNull().Throw();

            this.unitOfWork = unitOfWork;
        }

        protected Func<IUnitOfWorkEF> UnitOfWork
        {
            get { return this.unitOfWork; }
        }
    }
}
