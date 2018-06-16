using CarsCore.Core;
using CarsCore.Models;

namespace CarsCore.Persistance
{
    public class FeaturesRepository : Repository<Feature>,IFeaturesRepository
    {
        public FeaturesRepository(CarsDbContext context) : base(context)
        {
        }
    }
}