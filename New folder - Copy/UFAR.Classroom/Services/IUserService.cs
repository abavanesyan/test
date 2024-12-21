// Services/IUserService.cs
using UFAR.Classroom.Entities;


public interface IUserService
{
    Task<User> RegisterAsync(User user, string password);
    Task<User?> LoginAsync(string email, string password);
    Task<bool> UserExistsAsync(string email);
}
