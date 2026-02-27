using Auth_Task.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Auth_Task;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Presentation-layer services: authentication state, authorization, and Blazor auth integration.
    /// </summary>
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddAuthenticationCore();
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();

        services.AddScoped<CustomAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider =>
            provider.GetRequiredService<CustomAuthenticationStateProvider>());

        return services;
    }
}
