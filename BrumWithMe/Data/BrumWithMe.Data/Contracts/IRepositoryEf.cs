using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BrumWithMe.Data.Contracts
{
    public interface IRepositoryEf<T> where T : class
    {
        IQueryable<T> All { get; }

        IEnumerable<T> GetAll();

        IEnumerable<T1> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> selectExpression);

        IEnumerable<T1> GetAll<T1>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, T1>> project,
            params Expression<Func<T, object>>[] includes);

        T GetFirst(Expression<Func<T, bool>> filter);

        T GetFirst(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        void AddOrUpdate(T entity);
    }
}
