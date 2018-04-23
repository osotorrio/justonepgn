using System.Security.Cryptography;
using System.Text;

namespace JustOnePgn.Core.Services
{
    public class Sha256Service
    {
        private static SHA256 _instance;

        protected Sha256Service() { }

        public static string GetHash(string input)
        {
            if (_instance == null)
            {
                _instance = SHA256Managed.Create();
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
