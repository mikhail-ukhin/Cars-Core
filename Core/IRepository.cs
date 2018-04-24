using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarsCore.Core
{
    public interface IRepository<T> where T : class
    {
         Task<T> GetAsync(int id);
         Task<List<T>> GetAllAsync();

         Task AddAsync(T model);
         void Remove(T model);
    }
}