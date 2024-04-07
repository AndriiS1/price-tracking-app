using System.Security.Cryptography;
using System.Text;
using Domain.Services;

namespace Infrastructure.Services;

public class HashService : IHashService
{
    public string GetHash(string text)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}