using System;
using System.Security.Cryptography;
using System.Text;

namespace music_player.Helpers;

public class PasswordHasher
{
    private static readonly byte[] hmacKey;

    static PasswordHasher()
    {
        hmacKey = Encoding.UTF8.GetBytes("secure_key");
    }
    
    public static string HashPassword(string password)
    {
        using (var hmac = new HMACSHA512(hmacKey))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash); 
        }
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        using (var hmac = new HMACSHA512(hmacKey))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            string computedHashString = Convert.ToBase64String(computedHash);
            return computedHashString == storedHash;
        }
    }
}