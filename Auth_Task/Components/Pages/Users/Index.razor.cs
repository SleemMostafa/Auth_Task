using Auth_Task.Application.Interfaces;
using Auth_Task.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Auth_Task.Components.Pages.Users;

public sealed partial class Index
{
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private List<User>? users;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        try
        {
            isLoading = true;
            users = await UserService.GetAllUsersAsync();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            isLoading = false;
        }
    }
}
