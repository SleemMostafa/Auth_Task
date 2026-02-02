using Auth_Task.Application.DTOs;
using Auth_Task.Application.Interfaces;

namespace Auth_Task.Infrastructure.Services;

public sealed class AccountService(IUserRepository userRepository) : IAccountService
{
    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        var user = await userRepository.GetUserByUsernameAsync(loginDto.Username);

        if (user == null)
        {
            return false;
        }

        if (user.Password != loginDto.Password)
        {
            return false;
        }

        if (!user.IsActive)
        {
            return false;
        }

        return true;
    }

    public Task LogoutAsync()
    {
        return Task.CompletedTask;
    }
}
