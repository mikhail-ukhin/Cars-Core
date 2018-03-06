using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsCore.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public IList<KeyValuePairResource> Features { get; set; }

        public VehicleResource()
        {
            this.Features = new List<KeyValuePairResource>();
        }
    }
}