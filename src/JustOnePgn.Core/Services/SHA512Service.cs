using System.Security.Cryptography;
using System.Text;

namespace JustOnePgn.Core.Services
{
    public class Sha512Service
    {
        private static SHA512 _instance;

        protected Sha512Service() { }

        public static string GetHash(string input)
        {
            if (_instance == null)
            {
                _instance = SHA512Managed.Create();
            }

            return GenerateHash(input);
        }

        private static string GenerateHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = _instance.ComputeHash(bytes);
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
