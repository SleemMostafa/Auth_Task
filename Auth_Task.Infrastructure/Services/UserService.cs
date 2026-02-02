using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;

namespace Auth_Task.Infrastructure.Services;

public sealed class UserService(IUserRepository userRepository) : IUserService
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
        user.CreationDate = DateTime.Now;
        return await userRepository.CreateUserAsync(user);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        if (await userRepository.UsernameExistsAsync(user.Username, user.Id))
        {
            return false;
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
