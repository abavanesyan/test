using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UFAR.AN.Data;
using UFAR.AN.Data.DAO;
using UFAR.AN.Data.Entities;

namespace UFAR.AN.Core.Services.Cars
{
    public class CarServices : ICarServices
    {
        private readonly ApplicationDbContext _context; // DbContext for interacting with the database

        public CarServices(ApplicationDbContext context)
        {
            _context = context; // Initializing the DbContext in the constructor
        }

        // Method to add a new car
        public async Task AddCarAsync(CarsEntity car)
        {
            try
            {
                _context.Cars.Add(car); // Add the car entity to the Cars DbSet
                await _context.SaveChangesAsync(); // Save changes to the database
                Console.WriteLine("Car added successfully."); // Log success message
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding car: {ex.Message}"); // Log error message
            }
        }

        // Method to delete a car
        public async Task DeleteCarAsync(int carId)
        {
            try
            {
                var car = await _context.Cars.FindAsync(carId); // Find the car by its ID
                if (car != null)
                {
                    _context.Cars.Remove(car); // Remove the car entity
                    await _context.SaveChangesAsync(); // Save changes to the database
                    Console.WriteLine("Car deleted successfully."); // Log success message
                }
                else
                {
                    Console.WriteLine("Car not found."); // Log message if car not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting car: {ex.Message}"); // Log error message
            }
        }

        // Method to get all cars
        public async Task<IEnumerable<CarsEntity>> GetAllCarsAsync()
        {
            try
            {
                return await _context.Cars.ToListAsync(); // Retrieve all cars from the database
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all cars: {ex.Message}"); // Log error message
                return null;
            }
        }

        // Method to update a car
        public async Task UpdateCarAsync(int carId, CarsEntity updatedCar)
        {
            try
            {
                var car = await _context.Cars.FindAsync(carId); // Find the car by its ID
                if (car != null)
                {
                    // Update car properties with values from updatedCar
                    car.Name = updatedCar.Name;
                    car.Model = updatedCar.Model;
                    car.Year = updatedCar.Year;
                    // Update other properties as needed

                    await _context.SaveChangesAsync(); // Save changes to the database
                    Console.WriteLine("Car updated successfully."); // Log success message
                }
                else
                {
                    Console.WriteLine("Car not found."); // Log message if car not found
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating car: {ex.Message}"); // Log error message
            }
        }
    }
}
