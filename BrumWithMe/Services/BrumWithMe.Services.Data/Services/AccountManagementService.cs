using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
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
            : base(unitOfWork)
        {
            Guard.WhenArgument(carsRepo, nameof(carsRepo)).IsNull().Throw();
            Guard.WhenArgument(userRepo, nameof(userRepo)).IsNull().Throw();

            this.carsRepo = carsRepo;
            this.userRepo = userRepo;
        }

        public string GetUserAvatarUrl(string loggedUser)
        {
            Guard.WhenArgument(loggedUser, nameof(loggedUser)).IsNullOrEmpty().Throw();

            var userAvatarUrl = this.userRepo.GetById(loggedUser)?.AvataImageurl;

            return userAvatarUrl;
        }

        public void SetUserAvatar(string logedUserId, string imageUrl)
        {
            Guard.WhenArgument(logedUserId, nameof(logedUserId)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(imageUrl, nameof(imageUrl)).IsNullOrEmpty().Throw();

            using (var uow = base.UnitOfWork())
            {
                User user = this.userRepo.GetById(logedUserId);
                user.AvataImageurl = imageUrl;

                uow.Commit();
            }
        }
    }
}
