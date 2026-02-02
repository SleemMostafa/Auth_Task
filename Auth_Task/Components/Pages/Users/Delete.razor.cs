using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Delete 
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private User? user;
    private string errorMessage = string.Empty;
    private bool isDeleting = false;
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

    private async Task ConfirmDelete()
    {
        try
        {
            isDeleting = true;
            errorMessage = string.Empty;

            var result = await UserService.DeleteUserAsync(Id);

            if (result)
            {
                Navigation.NavigateTo("/users");
            }
            else
            {
                errorMessage = "Failed to delete user. Please try again.";
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            isDeleting = false;
            StateHasChanged();
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/users");
    }
}
