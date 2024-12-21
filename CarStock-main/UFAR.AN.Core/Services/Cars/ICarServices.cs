using System.Collections.Generic;
using System.Threading.Tasks;
using UFAR.AN.Data.Entities;

namespace UFAR.AN.Core.Services.Cars
{
    public interface ICarServices
    {
        // Method to get all cars asynchronously
        Task<IEnumerable<CarsEntity>> GetAllCarsAsync();

        // Method to add a new car asynchronously
        Task AddCarAsync(CarsEntity car);

        // Method to delete a car by its ID asynchronously
        Task DeleteCarAsync(int carId);

        // Method to update a car asynchronously
        Task UpdateCarAsync(int carId, CarsEntity updatedCar);
    }
}
