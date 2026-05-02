using System.Security.Cryptography;
using System.Text;

namespace Inventory_System_with_Security.Services;

public interface IAesEncryptionService
{
    string Encrypt(string plaintext);
    string Decrypt(string encryptedBase64);
}

public class AesEncryptionService(IConfiguration configuration) : IAesEncryptionService
{
    private readonly byte[] _key = Convert.FromBase64String(configuration["Security:AesKey"]
        ?? throw new InvalidOperationException("Missing Security:AesKey"));

    public string Encrypt(string plaintext)
    {
        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = _key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        var payload = new byte[aes.IV.Length + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, payload, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherBytes, 0, payload, aes.IV.Length, cipherBytes.Length);

        return Convert.ToBase64String(payload);
    }

    public string Decrypt(string encryptedBase64)
    {
        var payload = Convert.FromBase64String(encryptedBase64);
        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = _key;

        var iv = payload.Take(16).ToArray();
        var cipher = payload.Skip(16).ToArray();
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }
}
