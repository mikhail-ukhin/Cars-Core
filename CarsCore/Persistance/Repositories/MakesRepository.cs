using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsCore.Core;
using CarsCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsCore.Persistance.Repositories
{
    public class MakesRepository : Repository<Make>, IMakesRepository
    {
        public MakesRepository(CarsDbContext context) : base(context)
        {

        }

        public async Task<List<Make>> GetAllWithModelsAsync()
        {
            return await _context.Makes.Include(m => m.Models).ToListAsync();
        }
    }
}