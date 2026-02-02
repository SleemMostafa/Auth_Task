using Auth_Task.Application.DTOs;
using Auth_Task.Services;
using Microsoft.AspNetCore.Components;
using IAccountService = Auth_Task.Application.Interfaces.IAccountService;

namespace Auth_Task.Components.Pages;

public sealed partial class Login
{
    [Inject]
    private IAccountService AccountService { get; set; } = default!;

    [Inject]
    private CustomAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [SupplyParameterFromForm]
    private LoginDto? loginDto { get; set; }
    
    private string errorMessage = string.Empty;
    private bool isLoading = false;

    protected override void OnInitialized()
    {
        loginDto ??= new LoginDto();
    }

    private async Task HandleLogin()
    {
        if (loginDto == null) return;
        
        try
        {
            isLoading = true;
            errorMessage = string.Empty;
            StateHasChanged();

            var result = await AccountService.LoginAsync(loginDto);

            if (result)
            {
                await AuthStateProvider.MarkUserAsAuthenticated(loginDto.Username);
                Navigation.NavigateTo("/", forceLoad: true);
            }
            else
            {
                errorMessage = "Invalid username or password, or account is inactive.";
            }
        }
        catch (Exception ex)
        {
            errorMessage = "An error occurred during login. Please try again.";
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
}
