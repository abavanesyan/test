using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFAR.AN.Core.Services.CarParts; // Import the car part manager interface
using UFAR.AN.Data.Entities;

namespace UFAR.AN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ICarPartManager _carPartManager; // Dependency injection for car part manager

        public ItemsController(ICarPartManager carPartManager)
        {
            _carPartManager = carPartManager; // Initializing car part manager in constructor
        }

        // Endpoint to get all car parts
        [HttpGet]
        public async Task<IActionResult> GetAllCarParts()
        {
            var carParts = await _carPartManager.GetAllCarPartsAsync(); // Call manager to get all car parts
            return Ok(carParts); // Return the list of car parts
        }

        // Endpoint to add a new car part
        [HttpPost]
        public async Task<IActionResult> AddCarPart(ItemEntity carPart)
        {
            await _carPartManager.AddCarPartAsync(carPart); // Call manager to add a new car part
            return Ok(); // Return success status
        }

        // Endpoint to update an existing car part
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarPart(int id, ItemEntity carPart)
        {
            await _carPartManager.UpdateCarPartAsync(id, carPart); // Call manager to update a car part
            return Ok(); // Return success status
        }

        // Endpoint to delete a car part
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarPart(int id)
        {
            await _carPartManager.DeleteCarPartAsync(id); // Call manager to delete a car part
            return Ok(); // Return success status
        }
    }
}
