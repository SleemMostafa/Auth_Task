using Auth_Task.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth_Task.Infrastructure.Data.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(e => e.Username)
            .IsUnique()
            .HasDatabaseName("IX_Users_Username");

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.UserFullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.DateOfBirth)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(e => e.CreationDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasData(
            new User
            {
                Id = "1",
                Username = "admin",
                Password = "$pbkdf2-sha256$100000$U2VlZEFkbWluU2FsdDEyMw==$Z82h9Zs5auVHK60FJmF1IxtOxx4COmdhfFTjuemdtOg=",
                UserFullName = "Administrator",
                IsActive = true,
                DateOfBirth = new DateTime(1990, 1, 1),
                CreationDate = new DateTime(2026, 1, 1)
            }
        );
    }
}
