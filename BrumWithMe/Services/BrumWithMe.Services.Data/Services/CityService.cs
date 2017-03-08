﻿using System;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Models.Entities;
using BrumWithMe.Services.Data.Contracts;
using Bytes2you.Validation;
using BrumWithMe.Services.Providers.Mapping.Contracts;

namespace BrumWithMe.Services.Data.Services
{
    public class CityService : BaseDataService, ICityService
    {
        private readonly IRepository<City> cityRepo;

        public CityService(IRepository<City> cityRepo, Func<IUnitOfWork> unitOfWork, IMappingProvider mappingProvider)
            : base(unitOfWork, mappingProvider)
        {
            Guard.WhenArgument(cityRepo, nameof(cityRepo)).IsNull().Throw();

            this.cityRepo = cityRepo;
        }

        public City GetCityByName(string cityName)
        {
            var city = this.cityRepo.GetFirst(x => x.Name == cityName);

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

                uow.Commit();

                return city;
            }
        }
    }
}
