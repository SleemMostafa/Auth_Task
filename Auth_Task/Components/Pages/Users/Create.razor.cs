using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Create
{
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private User user = new() { DateOfBirth = DateTime.Today.AddYears(-20), IsActive = true };
    private string errorMessage = string.Empty;
    private string successMessage = string.Empty;
    private bool isSubmitting = false;

    private async Task HandleSubmit()
    {
        try
        {
            isSubmitting = true;
            errorMessage = string.Empty;
            successMessage = string.Empty;

            // Validate date of birth is in the past
            if (user.DateOfBirth >= DateTime.Today)
            {
                errorMessage = "Date of birth must be a past date.";
                return;
            }

            var result = await UserService.CreateUserAsync(user);

            if (result)
            {
                successMessage = "User created successfully!";
                await Task.Delay(1000);
                Navigation.NavigateTo("/users");
            }
            else
            {
                errorMessage = "Failed to create user. Username may already exist.";
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            isSubmitting = false;
            StateHasChanged();
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/users");
    }
}
