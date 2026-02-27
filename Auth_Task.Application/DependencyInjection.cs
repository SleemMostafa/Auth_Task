using Microsoft.Extensions.DependencyInjection;

namespace Auth_Task.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Application-layer services.
    /// Add application-level service registrations (validators, mediators, etc.) here as the project grows.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
