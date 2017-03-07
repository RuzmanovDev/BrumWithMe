using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
