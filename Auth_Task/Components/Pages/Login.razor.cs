using Auth_Task.Application.DTOs;
using Auth_Task.Services;
using Microsoft.AspNetCore.Components;
using IAccountService = Auth_Task.Application.Interfaces.IAccountService;

namespace Auth_Task.Components.Pages;

public sealed partial class Login
{
    [Inject]
    public required IAccountService AccountService { get; set; }

    [Inject]
    public required CustomAuthenticationStateProvider AuthStateProvider { get; set; }

    [Inject]
    public required NavigationManager Navigation { get; set; }

    [SupplyParameterFromForm]
    private LoginDto? LoginDto { get; set; }
    
    private string _errorMessage = string.Empty;
    private bool _isLoading;

    protected override void OnInitialized()
    {
        LoginDto ??= new LoginDto();
    }

    private async Task HandleLogin()
    {
        if (LoginDto == null) return;
        
        try
        {
            _isLoading = true;
            _errorMessage = string.Empty;
            StateHasChanged();

            var result = await AccountService.LoginAsync(LoginDto);

            if (result)
            {
                await AuthStateProvider.MarkUserAsAuthenticated(LoginDto.Username);
                Navigation.NavigateTo("/", forceLoad: true);
            }
            else
            {
                _errorMessage = "Invalid username or password, or account is inactive.";
            }
        }
        catch (Exception ex)
        {
            _errorMessage = "An error occurred during login. Please try again.";
        }
        finally
        {
            _isLoading = false;
            StateHasChanged();
        }
    }
}
