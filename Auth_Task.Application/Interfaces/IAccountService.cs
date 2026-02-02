using Auth_Task.Application.DTOs;

namespace Auth_Task.Application.Interfaces;

public interface IAccountService
{
    Task<bool> LoginAsync(LoginDto loginDto);
    Task LogoutAsync();
}
