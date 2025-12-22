using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Domain.Security;

namespace VendingMachines.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Создает безопасный хеш пароля с автоматически сгенерированной солью.
        /// </summary>
        /// <param name="password">Пароль в открытом виде.</param>
        /// <returns>Хеш пароля, готовый для хранения в БД.</returns>
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Проверяет предоставленный пароль на соответствие сохраненному хешу.
        /// </summary>
        /// <param name="providedPassword">Пароль, введенный пользователем.</param>
        /// <param name="hashedPassword">Хеш, извлеченный из базы данных.</param>
        /// <returns>True, если пароли совпадают, иначе False.</returns>
        public bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}
