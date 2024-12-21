using System.ComponentModel.DataAnnotations;

namespace UFAR.AN.Data.Entities
{
    /// <summary>
    /// Represents a car entity in the system.
    /// </summary>
    public class CarsEntity : BaseEntity
    {
        // Name of the car
        public string Name { get; set; }

        // Description of the car
        public string Description { get; set; }

        // Start VIN of the car
        public string StartVin { get; set; }

        // Manufacturer of the car
        public string Manufacturer { get; set; }

        // Year of the car
        public int Year { get; set; }

        // Model of the car
        public string Model { get; set; }
    }
}
