using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            Features = new List<int>();
        }

    }
}