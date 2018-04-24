using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarsCore.Core;
using CarsCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsCore.Persistance
{
    public class VehicleRepository : Repository<Vehicle>, IVehiclesRepository
    {
        public VehicleRepository(CarsDbContext context) : base(context)
        {
        }

        public async Task<Vehicle> GetVehicleWithMakesAndFeaturesAsync(int id)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);

            return vehicle;
        }
    }
}