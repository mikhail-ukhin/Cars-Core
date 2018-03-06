using System.Collections.Generic;
using CarsCore.Models;

namespace CarsCore.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public IList<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models = new List<KeyValuePairResource>();
        }
    }
}