using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Delete 
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    [Inject]
    public required IUserService UserService { get; init; } 

    [Inject]
    public required NavigationManager Navigation { get; init; }

    private User? _user;
    private string _errorMessage = string.Empty;
    private bool _isDeleting;
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

    private async Task ConfirmDelete()
    {
        try
        {
            _isDeleting = true;
            _errorMessage = string.Empty;

            var result = await UserService.DeleteUserAsync(Id);

            if (result)
            {
                Navigation.NavigateTo("/users");
            }
            else
            {
                _errorMessage = "Failed to delete user. Please try again.";
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            _isDeleting = false;
            StateHasChanged();
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("/users");
    }
}
