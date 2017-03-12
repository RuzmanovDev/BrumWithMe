using System;
using System.Collections.Generic;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Data.Models.CompositeModels;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public class CarService : BaseDataService, ICarService
    {
        private readonly IProjectableRepositoryEf<Car> carRepo;

        public CarService(IProjectableRepositoryEf<Car> carRepo, Func<IUnitOfWorkEF> unitOfWork)
            : base(unitOfWork)
        {
            Guard.WhenArgument(carRepo, nameof(carRepo)).IsNull().Throw();

            this.carRepo = carRepo;
        }

        public void AddCarToUser(Car car, string userId)
        {
            Guard.WhenArgument(car, nameof(car)).IsNull().Throw();
            Guard.WhenArgument(userId, nameof(userId)).IsNullOrEmpty().Throw();

            using (var uow = base.UnitOfWork())
            {
                car.OwenerId = userId;
                this.carRepo.Add(car);
                uow.Commit();
            }
        }

        public IEnumerable<CarBasicInfo> GetUserCars(string userId)
        {
            Guard.WhenArgument(userId, nameof(userId)).IsNullOrEmpty().Throw();

            return this.carRepo.GetAllMapped<CarBasicInfo>(w => !w.IsDeleted && w.OwenerId == userId);
        }
    }
}
