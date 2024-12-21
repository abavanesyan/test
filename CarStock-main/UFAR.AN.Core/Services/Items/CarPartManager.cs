using Microsoft.EntityFrameworkCore;
using UFAR.AN.Data.DAO;
using UFAR.AN.Data.Entities;

namespace UFAR.AN.Core.Services.CarParts
{
    public class CarPartManager : ICarPartManager
    {
        private readonly ApplicationDbContext _context; // DbContext for interacting with the database

        public CarPartManager(ApplicationDbContext context)
        {
            _context = context; // Initializing the DbContext in the constructor
        }

        // Method to add a new car part
        public async Task AddCarPartAsync(ItemEntity carPart) // Updated method parameter
        {
            try
            {
                await _context.Items.AddAsync(carPart); // Adding a new car part to the Items DbSet
                await _context.SaveChangesAsync(); // Save changes to the database
                Console.WriteLine("Car part added successfully."); // Log success message
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding car part: {ex.Message}"); // Log error message
            }
        }

        // Method to delete a car part
        public async Task DeleteCarPartAsync(int carPartId) // Updated method parameter
        {
            try
            {
                var carPart = await _context.Items.FindAsync(carPartId); // Find the car part by its ID
                if (carPart != null)
                {
                    _context.Items.Remove(carPart); // Remove the car part entity
                    await _context.SaveChangesAsync(); // Save changes to the database
                    Console.WriteLine("Car part deleted successfully."); // Log success message
                }
                else
                {
                    Console.WriteLine("Car part not found."); // Log message if car part not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting car part: {ex.Message}"); // Log error message
            }
        }

        // Method to get all car parts
        public async Task<IEnumerable<ItemEntity>> GetAllCarPartsAsync() // Updated method return type
        {
            try
            {
                return await _context.Items.ToListAsync(); // Retrieve all car parts from the database
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all car parts: {ex.Message}"); // Log error message
                return null;
            }
        }

        // Method to update a car part
        public async Task UpdateCarPartAsync(int carPartId, ItemEntity updatedCarPart) // Updated method parameters
        {
            try
            {
                var carPart = await _context.Items.FindAsync(carPartId); // Find the car part by its ID
                if (carPart != null)
                {
                    // Update car part properties with values from updatedCarPart
                    carPart.Name = updatedCarPart.Name;
                    carPart.Description = updatedCarPart.Description;
                    carPart.IsOEM = updatedCarPart.IsOEM;
                    carPart.OEM_Number = updatedCarPart.OEM_Number;
                    carPart.Condition = updatedCarPart.Condition;
                    carPart.Location = updatedCarPart.Location;
                    //carPart.Model = updatedCarPart.Model;
                    // Update other properties as needed

                    await _context.SaveChangesAsync(); // Save changes to the database
                    Console.WriteLine("Car part updated successfully."); // Log success message
                }
                else
                {
                    Console.WriteLine("Car part not found."); // Log message if car part not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating car part: {ex.Message}"); // Log error message
            }
        }
    }
}
