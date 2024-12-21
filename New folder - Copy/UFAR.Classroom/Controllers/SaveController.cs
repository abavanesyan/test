using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UFAR.Classroom.Entities;
using UFAR.Classroom;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;

[ApiController]
[Route("api/[controller]")]
public class AIResponseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AIResponseController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AIResponse>>> GetAIResponses()
    {
        return await _context.AIResponses.ToListAsync();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAIResponse(int id)
    {
        var aiResponse = await _context.AIResponses.FindAsync(id);
        if (aiResponse == null)
        {
            return NotFound();
        }

        _context.AIResponses.Remove(aiResponse);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("delete-all")]
    public async Task<IActionResult> DeleteAllAIResponses()
    {
        var aiResponses = await _context.AIResponses.ToListAsync();
        if (aiResponses == null || !aiResponses.Any())
        {
            return NotFound();
        }

        _context.AIResponses.RemoveRange(aiResponses);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
