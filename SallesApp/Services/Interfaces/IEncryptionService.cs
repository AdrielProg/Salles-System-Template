namespace SallesApp.Services.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        int? TryDecryptToInt(string cipherText);

    }
}
