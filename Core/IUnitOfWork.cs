using System;
using System.Threading.Tasks;

namespace CarsCore.Core
{
    public interface IUnitOfWork
    {
        IVehiclesRepository Vehicles { get; }
        IFeaturesRepository Features { get; }
        IMakesRepository Makes { get; }
        IModelsRepository Models { get; }

        Task CompleteAsync();
    }
}