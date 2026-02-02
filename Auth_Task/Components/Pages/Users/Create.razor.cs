using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Create
{
    [Inject]
    public required IUserService UserService { get; init; } 

    [Inject]
    public required NavigationManager Navigation { get; init; }

    private readonly User _user = new() { DateOfBirth = DateTime.Today.AddYears(-20), IsActive = true };
    private string _errorMessage = string.Empty;
    private string _successMessage = string.Empty;
    private bool _isSubmitting;

    private async Task HandleSubmit()
    {
        try
        {
            _isSubmitting = true;
            _errorMessage = string.Empty;
            _successMessage = string.Empty;

            // Validate date of birth is in the past
            if (_user.DateOfBirth >= DateTime.Today)
            {
                _errorMessage = "Date of birth must be a past date.";
                return;
            }

            var result = await UserService.CreateUserAsync(_user);

            if (result)
            {
                _successMessage = "User created successfully!";
                await Task.Delay(1000);
                Navigation.NavigateTo("/users");
            }
            else
            {
                _errorMessage = "Failed to create user. Username may already exist.";
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            _isSubmitting = false;
            StateHasChanged();
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/users");
    }
}
