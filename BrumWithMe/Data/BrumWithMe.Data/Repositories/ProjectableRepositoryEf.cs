﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BrumWithMe.Data.Contracts;

namespace BrumWithMe.Data.Repositories
{
    public class ProjectableRepositoryEf<T> : EfGenericRepository<T>, IProjectableRepositoryEf<T> where T : class
    {
        private readonly IMapper mapper;

        public ProjectableRepositoryEf(DbContext context, IMapper mapper)
            : base(context)
        {
            this.mapper = mapper;
        }

        public TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression)
        {
            var result = this.All.Where(filterExpression).ProjectToFirstOrDefault<TDestitanion>(this.mapper.ConfigurationProvider);

            return result;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>()
        {
            var result = this.All.ProjectToList<TDestination>(this.mapper.ConfigurationProvider);

            return result;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression)
        {
            var result = this.All.Where(filterExpression).ProjectToList<TDestination>(this.mapper.ConfigurationProvider);

            return result;
        }

        public IEnumerable<TDestination> GetAllMappedWithDescSort<T1, TDestination>(
             Expression<Func<T, bool>> filterExpression,
             Expression<Func<T, T1>> sort,
             int page, int size)
        {
            var result = this.All
                .Where(filterExpression)
                .OrderByDescending(sort)
                .Skip(page * size)
                .Take(size)
                .ProjectToList<TDestination>(this.mapper.ConfigurationProvider);

            return result;
        }

        public IEnumerable<TDestination> GetAllMapped<T1, TDestination>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, T1>> sort)
        {
            var result = this.All
                .Where(filterExpression)
                .OrderByDescending(sort)
                .ProjectToList<TDestination>(this.mapper.ConfigurationProvider);

            return result;
        }

        public IEnumerable<TDestination> GetAllMappedWithAscSort<T1, TDestination>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sort, int page, int size)
        {
            var result = this.All
                .Where(filterExpression)
                .OrderBy(sort)
                .ProjectToList<TDestination>(this.mapper.ConfigurationProvider);

            return result;
        }
    }
}
