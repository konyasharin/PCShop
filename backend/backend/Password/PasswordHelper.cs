using System.Security.Cryptography;
using System.Text;

namespace backend.Password
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(providedPassword));
                string hashedProvidedPassword = Convert.ToBase64String(hashedBytes);

                return hashedProvidedPassword == hashedPassword;
            }
        }
    }
}
