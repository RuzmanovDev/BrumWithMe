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
        private readonly IRepositoryEf<Car> carsRepo;
        private readonly IRepositoryEf<User> userRepo;

        public AccountManagementService(
            IRepositoryEf<Car> carsRepo,
            IRepositoryEf<User> userRepo,
            Func<IUnitOfWorkEF> unitOfWork)
            :base(unitOfWork)
        {
            Guard.WhenArgument(carsRepo, nameof(carsRepo)).IsNull().Throw();
            Guard.WhenArgument(userRepo, nameof(userRepo)).IsNull().Throw();

            this.carsRepo = carsRepo;
            this.userRepo = userRepo;
        }

        public string GetUserAvatarUrl(string loggedUser)
        {
            Guard.WhenArgument(loggedUser, nameof(loggedUser)).IsNull().Throw();

            var userAvatarUrl = this.userRepo.GetById(loggedUser).AvataImageurl;

            return userAvatarUrl;
        }
    }
}
