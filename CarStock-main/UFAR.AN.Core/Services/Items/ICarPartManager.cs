using System.Collections.Generic;
using System.Threading.Tasks;
using UFAR.AN.Data.Entities;

namespace UFAR.AN.Core.Services.CarParts
{
    public interface ICarPartManager
    {
        // Method to add a new car part asynchronously
        Task AddCarPartAsync(ItemEntity carPart);

        // Method to delete a car part by its ID asynchronously
        Task DeleteCarPartAsync(int carPartId);

        // Method to get all car parts asynchronously
        Task<IEnumerable<ItemEntity>> GetAllCarPartsAsync();

        // Method to update a car part asynchronously
        Task UpdateCarPartAsync(int carPartId, ItemEntity updatedCarPart);
    }
}
