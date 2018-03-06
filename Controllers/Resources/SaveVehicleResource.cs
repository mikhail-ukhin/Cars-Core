using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CarsCore.Models;

namespace CarsCore.Controllers.Resources
{

    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public IList<int> Features { get; set; }

        [Required]
        public ContactResource Contact { get; set; }

        public SaveVehicleResource()
        {
            this.Features = new List<int>();
        }

        public SaveVehicleResource(Vehicle vehicle)
        {
            this.Id = vehicle.Id;
            this.ModelId = vehicle.ModelId;
            this.IsRegistered = vehicle.IsRegistered;
            this.Features = vehicle.Features.Select(f => f.FeatureId).ToList();
        }
    }
}