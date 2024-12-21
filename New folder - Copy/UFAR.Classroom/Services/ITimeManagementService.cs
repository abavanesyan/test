using System.Collections.Generic;
using System.Threading.Tasks;
using UFAR.Classroom.Entities;
using UFAR.Classroom;

namespace UFAR.Classroom.Services
{
    public interface ITimeManagementService
    {
        Task<IEnumerable<Deadline>> GetAllDeadlinesAsync();
        Task<Deadline> GetDeadlineByIdAsync(int id);
        Task AddDeadlineAsync(Deadline deadline);
        Task DeleteDeadlineAsync(int id);
    }
}
