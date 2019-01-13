using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Peoplee.API.DataAccesLayer
{
    public class Repository<T> : IRepository<T> where T : class
    {
      
   private PeopleeContext dbContext;
        private DbSet<T> dbsetObject;
   
        public Repository(PeopleeContext context)
        {     
            dbContext = context;
            dbsetObject = dbContext.Set<T>(); 
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return  dbsetObject.FirstOrDefault(filter);
           // throw new NotImplementedException();
        }

        public List<T> GetList()
        {
           return dbsetObject.ToList();
           // throw new NotImplementedException();
        }

        public int Insert(T entity)
        {
         dbsetObject.Add(entity);
         return dbContext.SaveChanges();
           // throw new NotImplementedException();
        }

        public int Remove(T entity)
        {
            dbsetObject.Remove(entity);
            return dbContext.SaveChanges();
           // throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            //dbsetObject.Update(entity);
            return dbContext.SaveChanges();

        }

    }
}