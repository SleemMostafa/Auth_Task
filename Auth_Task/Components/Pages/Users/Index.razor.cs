using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Index
{
    [Inject]
    public required IUserService UserService { get; init; }

    [Inject]
    public required NavigationManager Navigation { get; init; }

    private List<User>? _users;
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        try
        {
            _isLoading = true;
            _users = await UserService.GetAllUsersAsync();
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            _isLoading = false;
        }
    }
}
