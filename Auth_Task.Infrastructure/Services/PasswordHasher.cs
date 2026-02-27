using System.Security.Cryptography;
using System.Text;
using Auth_Task.Application.Interfaces;

namespace Auth_Task.Infrastructure.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;
    private const string Prefix = "$pbkdf2-sha256$";

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return $"{Prefix}{Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (!hashedPassword.StartsWith(Prefix))
        {
            return false;
        }

        var parts = hashedPassword[Prefix.Length..].Split('$');
        if (parts.Length != 3 ||
            !int.TryParse(parts[0], out var iterations) ||
            iterations <= 0)
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(parts[1]);
            var storedHash = Convert.FromBase64String(parts[2]);

            var computedHash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                storedHash.Length);

            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
