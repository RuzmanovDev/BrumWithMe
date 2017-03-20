using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BrumWithMe.Data.Contracts
{
    public interface IProjectableRepositoryEf<T> : IRepositoryEf<T> where T : class
    {
        TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression);

        IEnumerable<TDestination> GetAllMapped<TDestination>();

        IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression);

        IEnumerable<TDestination> GetAllMapped<T1, TDestination>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, T1>> sort);

        IEnumerable<TDestination> GetAllMapped<T1, TDestination>(
                Expression<Func<T, bool>> filterExpression,
                Expression<Func<T, T1>> sort,
                int page, int size);
    }
}
