// Services/UserService.cs
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;
using UFAR.Classroom;



public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        if (await UserExistsAsync(user.Email))
            throw new Exception("User already exists.");

        user.PasswordHash = HashPassword(password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !VerifyPassword(password, user.PasswordHash))
            return null;

        return user;
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashed);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        var hash = HashPassword(password);
        return hash == storedHash;
    }
}
