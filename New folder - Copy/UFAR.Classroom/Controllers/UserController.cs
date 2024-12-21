// Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Models;
using UFAR.Classroom.Services;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
    {
        try
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email
            };

            await _userService.RegisterAsync(user, dto.Password);
            return Ok(new { Message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await _userService.LoginAsync(dto.Email, dto.Password);
        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        return Ok(new { Message = "Login successful", UserId = user.Id });
    }
}
