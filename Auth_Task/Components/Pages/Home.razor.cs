using Auth_Task.Application.Interfaces;
using Auth_Task.Services;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages;

public sealed partial class Home
{
    [Inject]
    public required IAccountService AccountService { get; init; }

    [Inject]
    public required CustomAuthenticationStateProvider AuthStateProvider { get; init; }

    [Inject]
    public required NavigationManager Navigation { get; init; }

    private async Task HandleLogout()
    {
        await AccountService.LogoutAsync();
        await AuthStateProvider.MarkUserAsLoggedOut();
        Navigation.NavigateTo("/login", forceLoad: true);
    }
}
