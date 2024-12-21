using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFAR.AN.Core.Services.Cars; // Import the car service interface
using UFAR.AN.Data.Entities; // Import the car entity

namespace UFAR.AN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarStockController : ControllerBase
    {
        private readonly ICarServices _carServices; // Dependency injection for car services

        public CarStockController(ICarServices carServices)
        {
            _carServices = carServices; // Initializing car service in constructor
        }

        // Endpoint to get all cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carServices.GetAllCarsAsync(); // Call service to get all cars
            return Ok(cars); // Return the list of cars
        }

        // Endpoint to add a new car
        [HttpPost]
        public async Task<IActionResult> AddCar(CarsEntity car)
        {
            await _carServices.AddCarAsync(car); // Call service to add a new car
            return Ok(); // Return success status
        }

        // Endpoint to update an existing car
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarsEntity car)
        {
            await _carServices.UpdateCarAsync(id, car); // Call service to update a car
            return Ok(); // Return success status
        }

        // Endpoint to delete a car
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carServices.DeleteCarAsync(id); // Call service to delete a car
            return Ok(); // Return success status
        }
    }
}
