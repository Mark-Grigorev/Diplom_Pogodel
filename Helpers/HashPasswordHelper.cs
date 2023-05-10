using System.Text;
using System.Security.Cryptography;

namespace Diplom_Pogodel.Helpers
{
    public class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create()) // Вычисляет хэш, при помощи алгоритка SHA256, для введенных данных, в нашем случае - пароль
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash; // И возвращает захэшированный пароль
            }


        }
    }
}
