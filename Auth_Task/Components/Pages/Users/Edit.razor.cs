using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Edit : ComponentBase
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    [Inject]
    public required IUserService UserService { get; init; } 

    [Inject]
    public required NavigationManager Navigation { get; init; } 

    private User? _user;
    private string _errorMessage = string.Empty;
    private string _successMessage = string.Empty;
    private bool _isSubmitting;
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadUser();
    }

    private async Task LoadUser()
    {
        try
        {
            _isLoading = true;
            _user = await UserService.GetUserByIdAsync(Id);
        }
        catch (Exception ex)
        {
            _errorMessage = $"Error loading user: {ex.Message}";

        }
        finally
        {
            _isLoading = false;
        }
    }

    private async Task HandleSubmit()
    {
        try
        {
            _isSubmitting = true;
            _errorMessage = string.Empty;
            _successMessage = string.Empty;

            if (_user == null) return;

            // Validate date of birth is in the past
            if (_user.DateOfBirth >= DateTime.Today)
            {
                _errorMessage = "Date of birth must be a past date.";
                return;
            }

            var result = await UserService.UpdateUserAsync(_user);

            if (result)
            {
                _successMessage = "User updated successfully!";
                await Task.Delay(1000);
                Navigation.NavigateTo("/users");
            }
            else
            {
                _errorMessage = "Failed to update user. Username may already exist.";
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
