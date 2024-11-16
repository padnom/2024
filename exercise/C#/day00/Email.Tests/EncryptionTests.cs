using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using FsCheck;
using VerifyXunit;

namespace Email.Tests;

public sealed class EncryptionTests
{
    private readonly Encryption _encryption = new(new Configuration(ConvertKey("Advent Of Craft"), ConvertIv("2024")));

    [Theory]
    [InlineData("EncryptedString.txt")]
    [InlineData("EncryptedEmail.txt")]
    public async Task Decrypt_An_Encrypted_String(string encryptedFilePath)
    {
        string decryptedText = _encryption.Decrypt(FileUtils.LoadFile(encryptedFilePath));
        await Verify(decryptedText).UseFileName(encryptedFilePath);
    }

    [Theory]
    [InlineData("DecryptedString.txt")]
    [InlineData("DecryptedEmail.txt")]
    public async Task Encrypt_A_String(string decryptedFilePath)
    {
        string encryptText = _encryption.Encrypt(FileUtils.LoadFile(decryptedFilePath));
        await Verify(encryptText).UseFileName(decryptedFilePath);
    }

    [Fact]
    public void EncryptDecrypt_ShouldReturnOriginalString()
        // Property-Based test ensuring decrypt(encrypt(x)) == x for non-empty strings
        => Prop.ForAll<string>(Arb.Default.String().Filter(str => !string.IsNullOrEmpty(str)),
                               plainText => _encryption.Decrypt(_encryption.Encrypt(plainText)) == plainText)
               .QuickCheckThrowOnFailure();

    private static string ConvertIv(string iv) => HashToBase64(iv, MD5.HashData);

    private static string ConvertKey(string key) => HashToBase64(key, SHA256.HashData);

    private static string HashToBase64(string input, Func<byte[], byte[]> hashFunc)
        => Convert.ToBase64String(hashFunc(Encoding.UTF8.GetBytes(input)));
}