using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCore.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}