using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Providers.FileUpload;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrumWithMe.Services.Data.Services
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IRepository<Car> carsRepo;
        private readonly IFileUploadProvider fileUploadProvider;
        private readonly Func<IUnitOfWork> unitOfWork;

        public AccountManagementService(
            IRepository<Car> carsRepo,
            IFileUploadProvider fileUploadProvider,
            Func<IUnitOfWork> unitOfWork)
        {
            Guard.WhenArgument(carsRepo, nameof(carsRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();

            this.carsRepo = carsRepo;
            this.unitOfWork = unitOfWork;
        }

        public void RegisterCarToUser(Car car)
        {
            this.carsRepo.Add(car);
        }

        public void CreateCar(Car car)
        {

        }

        public bool AddCarToUser(Car car, string userId)
        {
            using (var uow = this.unitOfWork())
            {
                car.OwenerId = userId;
                this.carsRepo.Add(car);
                return uow.Commit();
            }
        }
    }
}
