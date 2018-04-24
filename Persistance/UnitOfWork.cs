using System;
using System.Threading.Tasks;
using CarsCore.Core;
using CarsCore.Persistance.Repositories;

namespace CarsCore.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarsDbContext _context;

        public UnitOfWork(CarsDbContext context)
        {
            _context = context;

            Vehicles = new VehicleRepository(_context);
            Features = new FeaturesRepository(_context);
            Makes = new MakesRepository(_context);
            Models = new ModelsRepository(_context);
        }

        public IVehiclesRepository Vehicles { get; private set; }
        public IFeaturesRepository Features { get; private set; }
        public IMakesRepository Makes { get; private set; }
        public IModelsRepository Models { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}