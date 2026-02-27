using Auth_Task.Application.Interfaces;
using Auth_Task.Infrastructure.Data;
using Auth_Task.Infrastructure.Repositories;
using Auth_Task.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth_Task.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Infrastructure-layer services: database context, repositories, and domain services.
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)));

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    /// <summary>
    /// Applies infrastructure startup tasks (e.g., ensuring the database exists).
    /// Call this after <see cref="WebApplication.Build"/> using <c>app.Services.InitializeInfrastructure()</c>.
    /// </summary>
    public static IServiceProvider InitializeInfrastructure(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            dbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating database: {ex.Message}");
        }

        return serviceProvider;
    }
}
