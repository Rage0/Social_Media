using System.Security.Cryptography;
using System.Text;

namespace Social_Media.Web.Infrastructure.Static
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using(SHA256 mySHA256 = SHA256Managed.Create())
            {
                byte[] hash = mySHA256
                    .ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));

                StringBuilder hashSB = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    hashSB.Append(hash[i].ToString("x2"));
                }

                return hashSB.ToString();
            }
        }
    }
}
