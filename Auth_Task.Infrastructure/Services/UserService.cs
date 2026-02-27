using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;

namespace Auth_Task.Infrastructure.Services;

public sealed class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await userRepository.GetUserByIdAsync(id);
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        if (await userRepository.UsernameExistsAsync(user.Username))
        {
            return false;
        }

        user.Id = Guid.NewGuid().ToString();
        user.Password = passwordHasher.HashPassword(user.Password);
        user.CreationDate = DateTime.UtcNow;
        return await userRepository.CreateUserAsync(user);
    }

    public async Task<bool> UpdateUserAsync(User user, string? newPassword = null)
    {
        if (await userRepository.UsernameExistsAsync(user.Username, user.Id))
        {
            return false;
        }

        if (!string.IsNullOrWhiteSpace(newPassword))
        {
            user.Password = passwordHasher.HashPassword(newPassword);
        }
        else
        {
            var existing = await userRepository.GetUserByIdAsync(user.Id);
            if (existing == null)
            {
                return false;
            }
            user.Password = existing.Password;
        }

        return await userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        return await userRepository.DeleteUserAsync(id);
    }

    public async Task<bool> UsernameExistsAsync(string username, string? excludeUserId = null)
    {
        return await userRepository.UsernameExistsAsync(username, excludeUserId);
    }
}
