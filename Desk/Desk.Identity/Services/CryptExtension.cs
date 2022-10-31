using BCrypt.Net;

namespace Desk.Identity.Services;

public class CryptExtension
{
    public static string CreateHash(string input)
    {
        const int workFactor = 12;
        var hash = BCrypt.Net.BCrypt.HashPassword(input, workFactor);

        return hash;
    }

    public static bool Verify(string input, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(input, hash);
    }
}