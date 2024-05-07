using System.Security.Cryptography;
using System.Text;

namespace backend.Password
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Хэширует пароль с использованием алгоритма SHA-256.
        /// </summary>
        /// <param name="password">Пароль для хэширования.</param>
        /// <returns>Хэшированная строка пароля в формате Base64.</returns>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        
        
        /// <summary>
        /// Проверяет, соответствует ли предоставленный пароль хэшированному паролю.
        /// </summary>
        /// <param name="hashedPassword">Хэшированный пароль.</param>
        /// <param name="providedPassword">Предоставленный пароль для проверки.</param>
        /// <returns>True, если предоставленный пароль соответствует хэшированному паролю, иначе false.</returns>
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
