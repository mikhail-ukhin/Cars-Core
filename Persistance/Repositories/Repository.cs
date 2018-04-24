using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsCore.Core;
using Microsoft.EntityFrameworkCore;

namespace CarsCore.Persistance
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CarsDbContext _context;

        public Repository(CarsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T model)
        {
            await _context.AddAsync(model);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Remove(T model)
        {
            _context.Remove(model);
        }
    }
}