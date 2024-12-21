using Microsoft.EntityFrameworkCore;
using UFAR.AN.Core.Services.CarParts;
using UFAR.AN.Core.Services.Cars;
using UFAR.AN.Data.DAO;

namespace UFAR.AN.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext to the services and configure it to use SQL Server with the connection string from appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration
                .GetConnectionString("DefaultConnection")));

            // Registering services with their interfaces for dependency injection
            builder.Services.AddScoped<ICarServices, CarServices>(); // Register ICarServices with its implementation CarServices
            builder.Services.AddScoped<ICarPartManager, CarPartManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

            app.UseAuthorization(); // Add authorization middleware

            app.MapControllers(); // Map controller routes

            app.Run(); // Start the application
        }
    }
}
