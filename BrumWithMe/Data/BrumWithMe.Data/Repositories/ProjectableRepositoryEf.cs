using AutoMapper;
using BrumWithMe.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BrumWithMe.Data.Repositories
{
    public class ProjectableRepositoryEf<T> : EfGenericRepository<T>, IProjectableRepositoryEf<T> where T : class
    {
        private readonly IMapper mapper;

        public ProjectableRepositoryEf(BrumWithMeDbContext context, IMapper mapper)
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
    }
}
