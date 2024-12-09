using System.Security.Cryptography;

namespace OpenAiBlazorApp.Infrastructure.Security;
public class PasswordHasher
{
    public string HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));

        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static async Task<string> GenerateSecretKeyAsync()
    {
        return await Task.Run(() =>
        {
            var random = new RNGCryptoServiceProvider();
            var secretKey = new byte[32];
            random.GetBytes(secretKey);
            return Convert.ToBase64String(secretKey);
        });
    }
}
