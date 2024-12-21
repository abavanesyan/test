using Microsoft.EntityFrameworkCore;
using UFAR.Classroom;
using UFAR.Classroom.Services;
using UFAR.TimeManagementTracker.Backend.Services;

namespace UFAR.Classroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Swagger configuration for development.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add ApplicationDbContext to the services
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register application services
            builder.Services.AddScoped<IAIService, AIService>();
            builder.Services.AddScoped<ISubmissionService, SubmissionService>();
            builder.Services.AddScoped<ITimeManagementService, TimeManagementService>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline for development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable HTTPS redirection and authorization
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Map controllers (for API routes)
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
