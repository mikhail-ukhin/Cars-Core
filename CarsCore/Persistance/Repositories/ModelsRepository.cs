using CarsCore.Core;
using CarsCore.Models;

namespace CarsCore.Persistance.Repositories
{
    public class ModelsRepository : Repository<Model>, IModelsRepository
    {
        public ModelsRepository(CarsDbContext context) : base(context)
        {
        }
    }
}