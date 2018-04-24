using System.Collections.Generic;
using System.Threading.Tasks;
using CarsCore.Models;

namespace CarsCore.Core
{
    public interface IMakesRepository : IRepository<Make>
    {
         Task<List<Make>> GetAllWithModelsAsync();
    }
}