using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;
using UFAR.Classroom;

namespace UFAR.TimeManagementTracker.Backend.Services
{
    public class TimeManagementService : ITimeManagementService
    {
        private readonly ApplicationDbContext _context;

        public TimeManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Deadline>> GetAllDeadlinesAsync()
        {
            return await _context.Deadlines.ToListAsync();
        }

        public async Task<Deadline> GetDeadlineByIdAsync(int id)
        {
            return await _context.Deadlines.FindAsync(id);
        }

        public async Task AddDeadlineAsync(Deadline deadline)
        {
            _context.Deadlines.Add(deadline);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeadlineAsync(int id)
        {
            var deadline = await _context.Deadlines.FindAsync(id);
            if (deadline != null)
            {
                _context.Deadlines.Remove(deadline);
                await _context.SaveChangesAsync();
            }
        }
    }
}
