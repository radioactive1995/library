using library.application.Common.Interfaces.Authentication;
using System.Security.Cryptography;

namespace library.infrastructure.Providers.Authentication;
public class PasswordProvider : IPasswordProvider
{
    private static readonly int _saltSize = 16;
    private static readonly int _hashSize = 20;
    private static readonly int _iterations = 10000;

    public string HashPassword(string value)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);

        var pbkdf2 = new Rfc2898DeriveBytes(value, salt, _iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(_hashSize);

        var hashBytes = new byte[_saltSize + _hashSize];
        Array.Copy(salt, 0, hashBytes, 0, _saltSize);
        Array.Copy(hash, 0, hashBytes, _saltSize, _hashSize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var hashBytes = Convert.FromBase64String(hashedPassword);

        // Get salt from stored hash
        var salt = new byte[_saltSize];
        Array.Copy(hashBytes, 0, salt, 0, _saltSize);

        // Hash the input password with the salt
        var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, _iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(_hashSize);

        // Compare the result with the stored hash
        for (int i = 0; i < _hashSize; i++)
        {
            if (hashBytes[i + _saltSize] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
