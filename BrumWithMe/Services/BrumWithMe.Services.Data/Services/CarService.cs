﻿using System;
using System.Collections.Generic;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Data.Models.TransportEntities;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public class CarService : BaseDataService, ICarService
    {
        private readonly IRepository<Car> carRepo;

        public CarService(IRepository<Car> carRepo, Func<IUnitOfWork> unitOfWork)
            : base(unitOfWork)
        {
            Guard.WhenArgument(carRepo, nameof(carRepo)).IsNull().Throw();

            this.carRepo = carRepo;
        }
        public IEnumerable<CarBasicInfo> GetUserCars(string userId)
        {
            Guard.WhenArgument(userId, nameof(userId)).IsNullOrEmpty().Throw();

            return this.carRepo.GetAll(w => !w.IsDeleted && w.OwenerId == userId, s => new CarBasicInfo()
            {
                Id = s.Id,
                Make = s.Make,
                Model = s.Model,
                Year = s.Year,
                ImageUrl = s.ImageUrl,
                Color = s.Color
            });
        }
    }
}