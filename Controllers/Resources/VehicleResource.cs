using System;
using System.Collections.Generic;
using System.Linq;
using CarsCore.Models;

namespace CarsCore.Controllers.Resources
{

    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public IList<int> Features { get; set; }

        public ContactResource Contact { get; set; }

        public VehicleResource()
        {
            this.Features = new List<int>();
        }

        public VehicleResource(Vehicle vehicle)
        {
            this.Id = vehicle.Id;
            this.ModelId = vehicle.ModelId;
            this.IsRegistered = vehicle.IsRegistered;
            this.Features = vehicle.Features.Select(f => f.FeatureId).ToList();
        }
    }
}