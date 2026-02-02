using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Auth_Task.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth_Task.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await context.Users.OrderByDescending(u => u.CreationDate).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        try
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        try
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UsernameExistsAsync(string username, string? excludeUserId = null)
    {
        try
        {
            if (excludeUserId != null)
            {
                return await context.Users.AnyAsync(u => u.Username == username && u.Id != excludeUserId);
            }
            return await context.Users.AnyAsync(u => u.Username == username);
        }
        catch
        {
            return false;
        }
    }
}
