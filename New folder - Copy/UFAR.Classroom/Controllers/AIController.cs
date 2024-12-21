using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFAR.Classroom.Services;
using System.ComponentModel.DataAnnotations;
using UFAR.Classroom.Services;

namespace UFAR.Classroom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;

        public AIController(IAIService aiService)
        {
            _aiService = aiService;
        }

        // POST api/ai/ask-ai  
        [HttpPost("ask-ai")]
        public async Task<IActionResult> AskAI([FromBody, Required, MinLength(1)] string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Message cannot be empty.");
            }

            try
            {
                var response = await _aiService.GetAIResponseAsync(message);
                return Ok(new { response });  // Return a structured response  
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)  
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}