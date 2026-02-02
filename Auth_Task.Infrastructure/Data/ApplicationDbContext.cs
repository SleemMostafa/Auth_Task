using Auth_Task.Domain.Entities;
using Auth_Task.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Auth_Task.Infrastructure.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
