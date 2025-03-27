using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("CC1ACA3269FF4D2BBF88249C44E7885C");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("D10CF2FE3F84BE7A");

        /// <summary>
        /// Encrypt data
        /// </summary>
        /// <param name="plaintext">Data</param>
        /// <returns>Encrypted data</returns>
        public static string Encrypt(string plaintext)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plaintext);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt data
        /// </summary>
        /// <param name="ciphertext">Encrypted data</param>
        /// <returns>Decrypted data</returns>
        public static string Decrypt(string ciphertext)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(ciphertext)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}