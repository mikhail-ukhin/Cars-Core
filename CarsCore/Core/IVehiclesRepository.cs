using System.Threading.Tasks;
using CarsCore.Models;

namespace CarsCore.Core
{
    public interface IVehiclesRepository : IRepository<Vehicle>
    {
        Task<Vehicle> GetVehicleWithMakesAndFeaturesAsync(int id); 
    }
}