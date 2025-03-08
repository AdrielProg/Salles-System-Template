using System;
using SallesApp.Helpers;
using SallesApp.Services.Interfaces;

namespace SallesApp.Services
{
    public class EncryptionService : IEncryptionService
    {
        public string Encrypt(string plainText)
        {
            return EncryptionHelper.Encrypt(plainText);
        }

        public string Decrypt(string cipherText)
        {
            return EncryptionHelper.Decrypt(cipherText);
        }

        public int? TryDecryptToInt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return null;

            try
            {
                string decryptedValue = Decrypt(Uri.UnescapeDataString(cipherText));
                return int.Parse(decryptedValue);
            }
            catch
            {
                return null; 
            }
        }
    }
}
