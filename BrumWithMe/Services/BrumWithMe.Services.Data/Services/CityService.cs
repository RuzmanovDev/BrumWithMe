﻿using System;
using System.Collections.Generic;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Services.Data.Services
{
    public class CityService : BaseDataService, ICityService
    {
        private readonly IRepositoryEf<City> cityRepo;

        public CityService(IRepositoryEf<City> cityRepo, Func<IUnitOfWorkEF> unitOfWork)
            : base(unitOfWork)
        {
            Guard.WhenArgument(cityRepo, nameof(cityRepo)).IsNull().Throw();

            this.cityRepo = cityRepo;
        }

        public City GetCityByName(string cityName)
        {
            cityName = cityName?.ToLower();

            var city = this.cityRepo.GetFirst(x => !x.IsDeleted && x.Name.ToLower() == cityName);

            return city;
        }

        public City CreateCity(string cityName)
        {
            Guard.WhenArgument(cityName, nameof(cityName)).IsNullOrEmpty().Throw();

            using (var uow = base.UnitOfWork())
            {
                var city = new City()
                {
                    Name = cityName
                };

                this.cityRepo.Add(city);

                uow.Commit();

                return city;
            }
        }

        public IEnumerable<string> GetAllCityNames()
        {
            return this.cityRepo.GetAll(x => !x.IsDeleted, x => x.Name);
        }
    }
}
