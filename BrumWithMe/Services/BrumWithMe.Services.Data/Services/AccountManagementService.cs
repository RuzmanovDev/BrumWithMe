using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.FileUpload;
using Bytes2you.Validation;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Services
{
    public class AccountManagementService : BaseDataService, IAccountManagementService
    {
        private readonly IRepository<Car> carsRepo;
        private readonly IFileUploadProvider fileUploadProvider;

        public AccountManagementService(
            IRepository<Car> carsRepo,
            IFileUploadProvider fileUploadProvider,
            Func<IUnitOfWork> unitOfWork, IMappingProvider mappingProvider)
            :base(unitOfWork, mappingProvider)
        {
            Guard.WhenArgument(carsRepo, nameof(carsRepo)).IsNull().Throw();

            this.carsRepo = carsRepo;
        }

    }
}
