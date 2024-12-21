using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using UFAR.Classroom.Entities;

namespace UFAR.Classroom
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AIResponse> AIResponses { get; set; }
        public DbSet<FileRecords> FileRecords { get; set; }
        public DbSet<Deadline> Deadlines { get; set; }
        public DbSet<Exam> Exams { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
