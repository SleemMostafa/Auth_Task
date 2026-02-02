using Auth_Task.Domain.Entities;

namespace Auth_Task.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<bool> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(string id);
    Task<bool> UsernameExistsAsync(string username, string? excludeUserId = null);
}
