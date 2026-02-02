using Auth_Task.Application.Interfaces;
using Auth_Task.Services;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages;

public sealed partial class Home
{
    [Inject]
    private IAccountService AccountService { get; set; } = default!;

    [Inject]
    private CustomAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private async Task HandleLogout()
    {
        await AccountService.LogoutAsync();
        await AuthStateProvider.MarkUserAsLoggedOut();
        Navigation.NavigateTo("/login", forceLoad: true);
    }
}
