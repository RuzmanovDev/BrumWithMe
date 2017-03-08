using System;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public abstract class BaseDataService
    {
        private readonly Func<IUnitOfWork> unitOfWork;

        protected BaseDataService(Func<IUnitOfWork> unitOfWork)
        {
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();

            this.unitOfWork = unitOfWork;
        }

        protected Func<IUnitOfWork> UnitOfWork
        {
            get { return this.unitOfWork; }
        }
    }
}
