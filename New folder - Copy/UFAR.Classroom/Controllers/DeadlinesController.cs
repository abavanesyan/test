using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;


namespace UFAR.Classroom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeadlinesController : ControllerBase
    {
        private readonly ITimeManagementService _timeManagementService;

        public DeadlinesController(ITimeManagementService timeManagementService)
        {
            _timeManagementService = timeManagementService;
        }

        // GET: api/deadlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deadline>>> GetAllDeadlines()
        {
            var deadlines = await _timeManagementService.GetAllDeadlinesAsync();
            return Ok(deadlines);
        }

        // GET: api/deadlines/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Deadline>> GetDeadlineById(int id)
        {
            var deadline = await _timeManagementService.GetDeadlineByIdAsync(id);

            if (deadline == null)
                return NotFound();

            return Ok(deadline);
        }

        // POST: api/deadlines
        [HttpPost]
        public async Task<ActionResult> AddDeadline(Deadline deadline)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _timeManagementService.AddDeadlineAsync(deadline);
            return CreatedAtAction(nameof(GetDeadlineById), new { id = deadline.Id }, deadline);
        }

        // DELETE: api/deadlines/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeadline(int id)
        {
            await _timeManagementService.DeleteDeadlineAsync(id);
            return NoContent();
        }
    }
}
