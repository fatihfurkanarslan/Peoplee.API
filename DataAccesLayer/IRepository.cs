using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Peoplee.API.DataAccesLayer
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> filter);

        List<T> GetList();

        int Insert(T entity);

        int Remove(T entity);

        int Update(T entity);
    }
}