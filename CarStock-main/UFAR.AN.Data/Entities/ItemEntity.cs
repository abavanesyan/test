namespace UFAR.AN.Data.Entities
{
    /// <summary>
    /// Represents a part entity in the system.
    /// </summary>
    public class ItemEntity : BaseEntity
    {
        public string Name { get; set; }  // Part Name
        public string Description { get; set; }  // Part Description

        public List<CarsEntity>? CarsFit { get; set; }  // Cars that this part fits into

        public bool IsOEM { get; set; } // Indicates if the part is OEM (Original Equipment Manufacturer)

        public string OEM_Number { get; set; } // OEM Number

        public string Condition { get; set; } // Condition of the part (e.g., New or Used)

        public string Location { get; set; }  // Warehouse Location where the part is stored
    }
}
