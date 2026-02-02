using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Edit : ComponentBase
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private User? user;
    private string errorMessage = string.Empty;
    private string successMessage = string.Empty;
    private bool isSubmitting = false;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadUser();
    }

    private async Task LoadUser()
    {
        try
        {
            isLoading = true;
            user = await UserService.GetUserByIdAsync(Id);
        }
        catch (Exception ex)
        {
            errorMessage = $"Error loading user: {ex.Message}";

        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task HandleSubmit()
    {
        try
        {
            isSubmitting = true;
            errorMessage = string.Empty;
            successMessage = string.Empty;

            if (user == null) return;

            // Validate date of birth is in the past
            if (user.DateOfBirth >= DateTime.Today)
            {
                errorMessage = "Date of birth must be a past date.";
                return;
            }

            var result = await UserService.UpdateUserAsync(user);

            if (result)
            {
                successMessage = "User updated successfully!";
                await Task.Delay(1000);
                Navigation.NavigateTo("/users");
            }
            else
            {
                errorMessage = "Failed to update user. Username may already exist.";
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
