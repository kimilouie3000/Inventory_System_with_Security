using System.Security.Cryptography;
using System.Text;

namespace Inventory_System_with_Security.Services;

public interface IHashService
{
    string ComputeSha256(string input);
}

public class HashService : IHashService
{
    public string ComputeSha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }
}
