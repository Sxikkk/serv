using System.Security.Cryptography;
using System.Text;

namespace Api.Middlewares;

public class Hash
{
    public static string Password(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = sha256.ComputeHash(buffer);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
    
    public static string ToUrlSafeBase64(byte[] bytes, bool trimPadding = false)
    {
        string result = Convert.ToBase64String(bytes)
            .Replace('+', '-')
            .Replace('/', '_');
        return trimPadding ? result.TrimEnd('=') : result;
    }
}