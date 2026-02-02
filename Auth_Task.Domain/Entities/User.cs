namespace Auth_Task.Domain.Entities;

public sealed class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserFullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime DateOfBirth { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
