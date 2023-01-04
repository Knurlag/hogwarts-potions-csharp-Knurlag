using System;
using System.Security.Cryptography;

namespace HogwartsPotions.Data;

public class PasswordHash
{
    public static string HashPassword(string password)
    {
        byte[] passwordHash;
        using (SHA256 sha256Hash = SHA256.Create())
        {
            passwordHash = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        return Convert.ToBase64String(passwordHash);
    }
}