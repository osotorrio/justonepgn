using JustOnePgn.Core.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace JustOnePgn.Core.Services
{
    public class HashService : IHashStrings
    {
        public string GenerateSHA256String(string input)
        {
            SHA256 sha256 = SHA256Managed.Create();
            return GenerateHash(sha256, input);
        }

        public string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            return GenerateHash(sha512, inputString);
        }

        private static string GenerateHash(HashAlgorithm algorithm, string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = algorithm.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
